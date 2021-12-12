using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behavior_Scripts
{
    public class BehaviorFactory
    {
        public static Behavior CreateBehavior(Behavior.BehaviorConfig config)
        {
            if (config.type == Behavior.BehaviorType.Cohesion)
                return new Cohesion(config);
            else if (config.type == Behavior.BehaviorType.Alignment)
                return new Alignment(config);
            else if (config.type == Behavior.BehaviorType.Avoidance)
                return new Avoidance(config);
            else if (config.type == Behavior.BehaviorType.Wander)
                return new Wander(config);
            else
                return new Complex(new List<Behavior.BehaviorConfig>());
        }
        public static Complex CreateComplexBehavior(List<Behavior.BehaviorConfig> behaviorConfigs)
        {
            return new Complex(behaviorConfigs);
        }
    }
}
