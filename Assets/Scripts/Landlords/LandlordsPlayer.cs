using System.Collections.Generic;

namespace Landlords
{
    public enum LandlordsStatus
    {
        landlord,
        farmer
    }

    public class LandlordsPalyer
    {
        public LandlordsStatus Status = LandlordsStatus.farmer; // 默认都是农民
        public List<LandlordsCard> Pokers;
    }

}
