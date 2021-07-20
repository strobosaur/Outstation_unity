using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Practical
{
    public class Functions
    {
        // APPROACH FUNCTION
        public static float Approach(float from, float to, float spd)
        {            
            if (from < to) 
            {
                return Math.Min((from + spd), to);
            } 
            else 
            {
                return Math.Max((from - spd), to);
            }
        }

        // CHANCE FUNCTION
        public static bool Chance(float input)
        {
            return (UnityEngine.Random.value < input);
        }

        public static float Lengthdir_x(float len, Vector3 dir)
        {
            dir = dir.normalized * len;
            return dir.x;
        }

        public static float Lengthdir_y(float len, Vector3 dir)
        {
            dir = dir.normalized * len;
            return dir.y;
        }
    }
}