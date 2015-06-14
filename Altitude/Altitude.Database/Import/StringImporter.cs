using System.Collections.Generic;
using Altitude.Domain;

namespace Altitude.Database.Import
{
    public class StringImporter
    {
        public void Import(IEnumerable<string> lines)
        {
            using (var context = new Context())
            {
                foreach (var line in lines)
                {
                    Position position;
                    if (!Position.TryParse(line, out position))
                        continue;

                    context.Measurements.Add(new Measurement(position));
                }

                context.SaveChanges();
            }
        }
    }
}