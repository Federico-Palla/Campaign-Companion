using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignCompanion.Core.Catalog.Classes
{
    public interface IPrerequisite;

    public class MinimumLevelPrerequisite : IPrerequisite
    {
        public int MinimumLevel { get; set; } = 0;
    }

    public class ClassPrerequisite : IPrerequisite
    {
        public string ClassTag { get; set; } = string.Empty;
    }

    public class FeaturePrerequisite : IPrerequisite
    {
        public string FeatureTag { get; set; } = string.Empty;
    }
}
