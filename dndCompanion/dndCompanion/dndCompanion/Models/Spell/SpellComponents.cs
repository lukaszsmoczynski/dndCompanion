using System;
using System.Collections.Generic;
using System.Text;

namespace dndCompanion.Models.Spell
{
    public class SpellComponents
    {
        public bool Verbal { get; set; } = false;
        public bool Somatic { get; set; } = false;
        public bool Material { get; set; } = false;

        public List<MaterialComponent> MaterialComponents { get; set; } = new List<MaterialComponent>();

        public SpellComponents(bool verbal, bool somatic, bool material, MaterialComponent[] materialComponent)
        {
            Verbal = verbal;
            Somatic = somatic;
            Material = material;
        }

        public SpellComponents()
        {
        }
    }
}
