using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice7
{
    /// <summary>
    /// Class created for one method
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Map method, given one positino and range, in other range can return the same position.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromSource"></param>
        /// <param name="toSource"></param>
        /// <param name="fromTarget"></param>
        /// <param name="toTarget"></param>
        /// <returns></returns>
        public static decimal Map(this decimal value, decimal fromSource, decimal toSource, decimal fromTarget, decimal toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }
    }
}
