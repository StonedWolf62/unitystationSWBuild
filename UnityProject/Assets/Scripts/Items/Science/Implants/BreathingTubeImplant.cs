using HealthV2;
using Mirror;

namespace Items.Implants.Organs
{
	public class BreathingTubeImplant : BodyPartFunctionality
	{
		[field: SyncVar] public bool isEMPed { get; private set; } = false;

		public override void AddedToBody(LivingHealthMasterBase livingHealth)
		{
			RelatedPart.HealthMaster.RespiratorySystem.AddImplant(this);
		}

		public override void RemovedFromBody(LivingHealthMasterBase livingHealth)
		{
			RelatedPart.HealthMaster.RespiratorySystem.RemoveImplant(this);
		}

		public override void EmpResult(int strength)
		{
			isEMPed = true;
		}
	}
}
