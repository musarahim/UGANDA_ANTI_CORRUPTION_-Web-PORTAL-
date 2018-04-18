using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Uganda_anti_corruption_portal.Areas.HelpPage.ModelDescriptions
{
    public class ParameterDescription
    {
        public ParameterDescription()
        {
            Annotations = new Collection<ParameterAnnotation>();
        }

        public Collection<ParameterAnnotation> Annotations { get; private set; }

        public string Documentation { get; set; }

        public string Name { get; set; }

        public ModelDescription TypeDescription { get; set; }
    }
}