using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Behavior_Scripts
{
    public abstract class TargetOrientedBehavior : Behavior
    {
        protected TargetOrientedBehavior(BehaviorConfig config, BehaviorAgent agent) : base(agent)
        {
            this.config = config;
        }
    }
}
