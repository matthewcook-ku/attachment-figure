using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using DG.Tweening;

// This class represents an organized and easy to navigate map of the character.
// NOTICE: this is not a MonoBehavior
public class AgentModel
{
    public enum Side
    {
        right = 0,
        left = 1,
        none
    };

    // Set of classes to build a model of the existing model.
    // Having these mapped in objects will let us access the parts much easier.

    // Base Class
    public abstract class SkinComponent<RigConstraint>
        where RigConstraint : IRigConstraint
    {
        // all models have a link to the main model object
        // this provides a central spot to go for access to all parts.
        public AgentModel Model;

        // tweener used to control weight
        Tweener WeightTweener;

        // define if this component has a side, or is singular
        public Side Side;

        private RigConstraint _Constraint; // backing field
        public RigConstraint Constraint // don't use to change weights please, use ConstraintWeight vars below
        {
            get { return _Constraint; }
            set
            {
                _Constraint = value;
                if(Constraint.weight > 0.0f) StoredConstraintWeight = Constraint.weight;
            }
        }
        private float StoredConstraintWeight = 1.0f; // if constraint is read in as 0.0, then default to 1.0 for stored weight
        public float ConstraintWeight
        {
            get { return Constraint.weight; }
            set
            {
                //Debug.Log("Setting Constraint Weight to " + value);
                StoredConstraintWeight = Constraint.weight;
                Constraint.weight = value;
                //Debug.Log("Constraint Weight is now " + Constraint.weight);
            }
        }
        public void TweenToConstraintWeight(float to, float duration)
        {
            //Debug.Log("Tweening Constraint Weight from: " + ConstraintWeight + " to: " + to);
            if (to == ConstraintWeight) return;

            // if there is a tween going, finish it now.
            if(WeightTweener.IsActive() && WeightTweener.IsPlaying()) WeightTweener.Complete();

            // store the constraint weight here and then change value directly
            // this way the current weight is stored, and not the last tweened weight before zero
            StoredConstraintWeight = Constraint.weight;
            WeightTweener = DOTween.To((value) => Constraint.weight = value, ConstraintWeight, to, duration);
        }
        public void TweenOnConstraint(float duration)
        {
            if (ConstraintActive) return;
            TweenToConstraintWeight(StoredConstraintWeight, duration);
        }
        public void TweenOffConstraint(float duration)
        {
            if (!ConstraintActive) return;
            TweenToConstraintWeight(0.0f, duration);
        }
        public bool ConstraintActive
        {
            get { return (Constraint.weight > 0.0f); }
            set
            {
                //Debug.Log("Setting Constraint To, with val: " + value + " " + StoredConstraintWeight);
                Constraint.weight = (value == false) ? 0.0f : StoredConstraintWeight;
                //Debug.Log("Weight is now: " + Constraint.weight);
            }
        }
        public bool ConstraintInTransition
		{
            get { return (WeightTweener.IsActive() && WeightTweener.IsPlaying()); }
		}
        public void ToggleConstraint()
        {
            bool currentlyActive = ConstraintActive;
            Debug.Log("Toggling Constraint: " + currentlyActive + " -> " + !currentlyActive);
            if (currentlyActive)
                ConstraintActive = false;
            else
                ConstraintActive = true;
        }
        public void TweenToggleConstraint(float duration)
        {
            bool currentlyActive = ConstraintActive;
            Debug.Log("Tween Toggling Constraint: " + currentlyActive + " -> " + !currentlyActive);
            if (currentlyActive)
                TweenToConstraintWeight(0.0f, duration);
            else
                TweenToConstraintWeight(StoredConstraintWeight, duration);
        }

        // constructor
        public SkinComponent(AgentModel model, Side side = Side.none)
        {
            this.Model = model;
            this.Side = side;
        }
    }
    // Concrete model components
    public class HandModel : SkinComponent<TwoBoneIKConstraint>
    {
        public GameObject Bone;
        public GameObject PlaceTarget;
        public GameObject Hint;

        // constructor
        public HandModel(AgentModel model, Side side) : base(model, side) {}
        public override string ToString()
        {
            string output = "";
            output += (AgentModel.Side.right == Side) ? "Right " : "Left ";
            output += "Hand: \n";
            output += "\t Bone - " + Bone.ToString() + "\n";
            output += "\t Constraint - " + this.Constraint.ToString() + "\n";
            output += "\t PlaceTarget - " + PlaceTarget.ToString() + "\n";
            output += "\t Hint - " + Hint.ToString() + "\n";

            return output;
        }
    }
    public class ArmModel : SkinComponent<TwoBoneIKConstraint>
    {
        public GameObject RootBone;
        public GameObject MidBone;
        public GameObject TipBone;

        // constructor
        public ArmModel(AgentModel model, Side side) : base(model, side) { }
        public override string ToString()
        {
            string output = "";
            output += (AgentModel.Side.right == Side) ? "Right " : "Left ";
            output += "Arm: \n";
            output += "\t RootBone - " + RootBone.ToString() + "\n";
            output += "\t MidBone - " + MidBone.ToString() + "\n";
            output += "\t TipBone - " + TipBone.ToString() + "\n";
            output += "\t Constraint - " + this.Constraint.ToString() + "\n";

            return output;
        }
    }
    public class HeadModel : SkinComponent<MultiAimConstraint>
    {
        public GameObject GazeTarget;
        public MultiParentConstraint GazeConstraint;
        public SkinnedMeshRenderer HeadMesh;
        public GameObject Bone;
        public GameObject LookTarget;

        public HeadModel(AgentModel model) : base(model) { }
        public override string ToString()
        {
            string output = "";
            output += "Head: \n";
            output += "\t GazeTarget - " + GazeTarget.ToString() + "\n";
            output += "\t GazeConstraint - " + GazeConstraint.ToString() + "\n";
            output += "\t HeadMesh - " + HeadMesh.ToString() + "\n";
            output += "\t Bone - " + Bone.ToString() + "\n";
            output += "\t Constraint - " + this.Constraint.ToString() + "\n";
            output += "\t LookTarget - " + LookTarget.ToString() + "\n";

            return output;
        }
    }
    public class NeckModel : SkinComponent<MultiAimConstraint>
    {
        public GameObject Bone;
        public GameObject LookTarget;

        public NeckModel(AgentModel model) : base(model) { }
        public override string ToString()
        {
            string output = "";
            output += "Neck: \n";
            output += "\t Bone - " + Bone.ToString() + "\n";
            output += "\t Constraint - " + this.Constraint.ToString() + "\n";
            output += "\t LookTarget - " + LookTarget.ToString() + "\n";

            return output;
        }
    }
    public class EyeModel : SkinComponent<MultiAimConstraint>
    {
        public GameObject Bone;
        public GameObject LookTarget;

        // constructor
        public EyeModel(AgentModel model, Side side) : base(model, side) { }
        public override string ToString()
        {
            string output = "";
            output += (AgentModel.Side.right == Side) ? "Right " : "Left ";
            output += "Eye: \n";
            output += "\t Bone - " + Bone.ToString() + "\n";
            output += "\t Constraint - " + this.Constraint.ToString() + "\n";
            output += "\t LookTarget - " + LookTarget.ToString() + "\n";

            return output;
        }
    }
    public class SpineModel : SkinComponent<ChainIKConstraint>
    {
        public GameObject RootBone;
        public GameObject TipBone;
        public GameObject LeanTarget;

        // constructor
        public SpineModel(AgentModel model) : base(model) { }
        public override string ToString()
        {
            string output = "";
            output += "Spine: \n";
            output += "\t RootBone - " + RootBone.ToString() + "\n";
            output += "\t TipBone - " + TipBone.ToString() + "\n";
            output += "\t Constraint - " + this.Constraint.ToString() + "\n";
            output += "\t LeanTarget - " + LeanTarget.ToString() + "\n";

            return output;
        }
    }

    // Model Components Belonging to the main model object.
    public SkinnedMeshRenderer BodyMesh;
    public GameObject SkinRootBone;
    public GameObject LookTarget;
    public MultiPositionConstraint LookTargetConstraint;
    public Vector3 LookTargetDefaultPosition { get; private set; }
    public void ResetLookTargetPosition() { LookTarget.transform.position = LookTargetDefaultPosition; }
    public HeadModel Head;
    public EyeModel[] Eye; // indexes are side enum left/right
    public EyeModel EyeLeft
    {
        get { return Eye[(int)Side.left]; }
        set { Eye[(int)Side.left] = value; }
    }
    public EyeModel EyeRight
    {
        get { return Eye[(int)Side.right]; }
        set { Eye[(int)Side.right] = value; }
    }
    public void ToggleBothEyeConstraints()
    {
        Eye[(int)Side.left].ToggleConstraint();
        Eye[(int)Side.right].ToggleConstraint();
    }
    public SpineModel Spine;
    public NeckModel Neck;
    public ArmModel[] Arm; // indexes are side enum left/right
    public ArmModel ArmLeft
    {
        get { return Arm[(int)Side.left]; }
        set { Arm[(int)Side.left] = value; }
    }
    public ArmModel ArmRight
    {
        get { return Arm[(int)Side.right]; }
        set { Arm[(int)Side.right] = value; }
    }

    public HandModel[] Hand; // indexes are side enum left/right
    public HandModel HandLeft 
    {
        get { return Hand[(int)Side.left]; }
        set { Hand[(int)Side.left] = value; }
    }
    public HandModel HandRight
    {
        get { return Hand[(int)Side.right]; }
        set { Hand[(int)Side.right] = value; }
    }
    public void ToggleBothHandConstraints()
    {
        Hand[(int)Side.right].ToggleConstraint();
        Hand[(int)Side.left].ToggleConstraint();
    }

    // This is not a monobehavior, but will be associated with a game object, so stash it here
    private GameObject gameObject;
    private RigBuilder rigBuilder;

    // Prefix to add when searching for mesh components
    public string SkinMeshPrefix;


    // Print out the model parts so we can check if they were done correctly
    private void PrintModel()
    {
        Debug.Log("Printing Model");
        Debug.Log("-" + Head.ToString());
        Debug.Log("-" + EyeLeft.ToString());
        Debug.Log("-" + EyeRight.ToString());
        Debug.Log("-" + Spine.ToString());
        Debug.Log("-" + Neck.ToString());
        Debug.Log("-" + ArmLeft.ToString());
        Debug.Log("-" + ArmRight.ToString());
        Debug.Log("-" + HandLeft.ToString());
        Debug.Log("-" + HandRight.ToString());
    }

    // Class Constructor
    // Since this is not a MonoBehavior, we need to do our own allocation.
    public AgentModel(GameObject obj, string prefix="")
    {
        //Debug.Log("Initializing Agent Model...");

        gameObject = obj;
        rigBuilder = obj.GetComponent<RigBuilder>();

        SkinMeshPrefix = prefix;

        Head = new HeadModel(this);
        Eye = new EyeModel[]
        {
            new EyeModel(this, Side.right),
            new EyeModel(this, Side.left),
        };
        Spine = new SpineModel(this);
        Neck = new NeckModel(this);
        Arm = new ArmModel[]
        {
            new ArmModel(this, Side.right),
            new ArmModel(this, Side.left),
        };
        Hand = new HandModel[]
        {
            new HandModel(this, Side.right),
            new HandModel(this, Side.left),
        };

        // traverse the model and fill in links to the objects
        Resolve(gameObject);

        // now print it out to check things look OK.
        //PrintModel();

        // TODO: would like to set up all the constraint settings for all parts at this point from script so it does not need to be set up in the inspector.
        // for now will do this by hand in the inspector. 
        // see: https://forum.unity.com/threads/changing-two-bone-ik-target-at-runtime-gives-many-warnings.972753/
    }

    public void Resolve(GameObject obj)
    {
        //Debug.Log("Resolving Model...");
        // find all the elements of the skeleton
        Transform skeleton = obj.transform.Find("GAME_rig-" + SkinMeshPrefix);
        ResolveSkeleton(skeleton.gameObject);

        // find all the elements of the Unity rig
        Transform rig = obj.transform.Find("AgentRig");
        ResolveRig(rig.gameObject);

        // find the skin's meshes
        ResolveMesh(obj);
    }    

    // These are basically a hard coded mapping of the current model structure and naming.
    private void ResolveSkeleton(GameObject obj)
    {
        //Debug.Log("\tResolving Skeleton...", obj);
        Transform transform = obj.transform;

        Transform skinRootBone = transform.Find("DEF-spine");
        Transform spineRootBone = skinRootBone.Find("DEF-spine.001");
        Transform clavBone = spineRootBone.Find("DEF-spine.002/DEF-spine.003");
        Transform neckBone = clavBone.Find("DEF-spine.004");

        Transform headBone = neckBone.Find("DEF-spine.005/DEF-spine.006");
        Transform eyeBoneR = headBone.Find("DEF-eye.R");
        Transform eyeBoneL = headBone.Find("DEF-eye.L");

        Transform armRootBoneR = clavBone.Find("DEF-shoulder.R/DEF-upper_arm.R");
        Transform armMidBoneR = armRootBoneR.Find("DEF-upper_arm.R.001/DEF-forearm.R");
        Transform handBoneR = armMidBoneR.Find("DEF-forearm.R.001/DEF-hand.R");

        Transform armRootBoneL = clavBone.Find("DEF-shoulder.L/DEF-upper_arm.L");
        Transform armMidBoneL = armRootBoneL.Find("DEF-upper_arm.L.001/DEF-forearm.L");
        Transform handBoneL = armMidBoneL.Find("DEF-forearm.L.001/DEF-hand.L");

        SkinRootBone = skinRootBone.gameObject;
        Spine.RootBone = spineRootBone.gameObject;
        Spine.TipBone = clavBone.gameObject;
        Neck.Bone = neckBone.gameObject;
        Head.Bone = headBone.gameObject;
        EyeLeft.Bone = eyeBoneL.gameObject;
        EyeRight.Bone = eyeBoneR.gameObject;
        ArmLeft.RootBone = armRootBoneL.gameObject;
        ArmLeft.MidBone = armMidBoneL.gameObject;
        ArmLeft.TipBone = handBoneL.gameObject;
        ArmRight.RootBone = armRootBoneR.gameObject;
        ArmRight.MidBone = armMidBoneR.gameObject;
        ArmRight.TipBone = handBoneR.gameObject;
        HandLeft.Bone = handBoneL.gameObject;
        HandRight.Bone = handBoneR.gameObject;

    }
    // These are basically a hard coded mapping of the current model structure and naming.
    private void ResolveRig(GameObject obj)
    {
        //Debug.Log("\tResolving Rig...", obj);
        Transform transform = obj.transform;

        Transform GazeTarget = transform.Find("Gaze Target");

        Transform LeanIK = transform.Find("LeanIK");
        Transform LeanTarget = LeanIK.Find("LeanTarget");

        Transform RHIK = transform.Find("RHIK");
        Transform TargetR = RHIK.Find("TargetR");
        Transform HintR = RHIK.Find("HintR");

        Transform LHIK = transform.Find("LHIK");
        Transform TargetL = LHIK.Find("TargetL");
        Transform HintL = LHIK.Find("HintL");

        Transform LookAt = transform.Find("LookAt");
        Transform NeckLookAt = LookAt.Find("NeckLookAt");
        Transform HeadLookAt = LookAt.Find("HeadLookAt");
        Transform EyeRLookAt = LookAt.Find("EyeRLookAt");
        Transform EyeLLookAt = LookAt.Find("EyeLLookAt");
        Transform LookTarget = LookAt.Find("LookTarget");
        Transform HeadLookTarget = LookTarget.Find("HeadLookTarget");
        Transform EyeLookTarget = LookTarget.Find("EyeLookTarget");

        Spine.Constraint = LeanIK.GetComponent<ChainIKConstraint>();
        Spine.LeanTarget = LeanTarget.gameObject;

        HandRight.Constraint = RHIK.GetComponent<TwoBoneIKConstraint>();
        HandRight.PlaceTarget = TargetR.gameObject;
        HandRight.Hint = HintR.gameObject;
        ArmRight.Constraint = HandRight.Constraint; // shared with hand
        HandLeft.Constraint = LHIK.GetComponent<TwoBoneIKConstraint>();
        HandLeft.PlaceTarget = TargetL.gameObject;
        HandLeft.Hint = HintL.gameObject;
        ArmLeft.Constraint = HandLeft.Constraint; // shared with hand

        Head.GazeTarget = GazeTarget.gameObject;
        Head.GazeConstraint = GazeTarget.gameObject.GetComponent<MultiParentConstraint>();
        this.LookTarget = LookTarget.gameObject;
        this.LookTargetDefaultPosition = LookTarget.position;
        LookTargetConstraint = LookTarget.gameObject.GetComponent<MultiPositionConstraint>();

        Head.Constraint = HeadLookAt.GetComponent<MultiAimConstraint>();
        Head.LookTarget = HeadLookTarget.gameObject;
        Neck.Constraint = NeckLookAt.GetComponent<MultiAimConstraint>();
        Neck.LookTarget = HeadLookTarget.gameObject; // same as head

        EyeLeft.Constraint = EyeLLookAt.GetComponent<MultiAimConstraint>();
        EyeLeft.LookTarget = EyeLookTarget.gameObject; // same for both
        EyeRight.Constraint = EyeRLookAt.GetComponent<MultiAimConstraint>();
        EyeRight.LookTarget = EyeLookTarget.gameObject; // same for both

    }
    // These are basically a hard coded mapping of the current model structure and naming.
    private void ResolveMesh(GameObject obj)
    {
        //Debug.Log("\tResolving Mesh...", obj);
        Transform transform = obj.transform;

        Transform HeadTransform = transform.Find(SkinMeshPrefix + "_Head");
        Head.HeadMesh = HeadTransform.GetComponent<SkinnedMeshRenderer>();

        Transform BodyTransform = transform.Find(SkinMeshPrefix + "_Body");
        BodyMesh = BodyTransform.GetComponent<SkinnedMeshRenderer>();
    }
}
