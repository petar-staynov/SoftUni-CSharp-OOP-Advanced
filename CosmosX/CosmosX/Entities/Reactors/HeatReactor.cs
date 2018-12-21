using CosmosX.Entities.Containers.Contracts;

namespace CosmosX.Entities.Reactors
{
    public class HeatReactor : BaseReactor
    {
        public HeatReactor(int id, IContainer moduleContainer, int heatReductionIndex)
            : base(id, moduleContainer)
        {
            this.HeatReductionIndex = heatReductionIndex;
        }

        public int HeatReductionIndex { get; private set; }


        public override long TotalEnergyOutput
        {
            get
            {
                if (base.TotalEnergyOutput > this.TotalHeatAbsorbing)
                {
                    return 0;
                }

                //TODO maybe return 0
                return base.TotalEnergyOutput;
            }
        }

        public override long TotalHeatAbsorbing
            => base.TotalHeatAbsorbing + this.HeatReductionIndex;
    }
}