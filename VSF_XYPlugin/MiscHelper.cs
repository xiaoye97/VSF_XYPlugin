using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VSF_XYPlugin
{
    public static class MiscHelper
    {
        public static Vector3 RandomV3(float randomLimit)
        {
            float x = Random.Range(-randomLimit, randomLimit);
            float y = Random.Range(-randomLimit, randomLimit);
            float z = Random.Range(-randomLimit, randomLimit);
            return new Vector3(x, y, z);
        }
    }
}
