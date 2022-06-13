using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VSF_XYPlugin
{
    public class DropItemBehaviour : MonoBehaviour
    {
        void Update()
        {
            if (transform.position.y < -10)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
