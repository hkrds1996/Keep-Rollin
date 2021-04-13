using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VolumeControler
{
    public class VControler
    {
        public static VControler single;

        VControler()
        {
            if(single==null)
            {
                single = new VControler();
            }
        }

        public static Vector2 GetXMinYMaxVector2FromVector2(Vector2 _vector2)
        {
            Vector2 tmp;

            tmp.x = Mathf.Min(_vector2.x, _vector2.y);
            tmp.y = Mathf.Max(_vector2.x, _vector2.y);

            return tmp;
        }

        /// <summary>
        /// 用_Value初始化数组_Group
        /// </summary>
        /// <param name="_Group"></param>
        /// <param name="_Value"></param>
        public static void Vector3GroupInitByVector3(Vector3[] _Group,Vector3 _Value)
        {
            for(int i=0;i<_Group.Length;i++)
            {
                _Group[i] = _Value;
            }
        }

        /// <summary>
        /// 把输入的_vector2Int的分量xy，以最小值为x，最大值为y，生成一个新的Vector2Int对象作为返回值。
        /// </summary>
        /// <param name="_vector2Int"></param>
        /// <returns></returns>
        public static Vector2Int GetXMinYMaxVectorInt2FromVector2Int(Vector2Int _vector2Int)
        {
            Vector2Int tmp=Vector2Int.zero;

            tmp.x = Mathf.Min(_vector2Int.x, _vector2Int.y);
            tmp.y = Mathf.Max(_vector2Int.x, _vector2Int.y);

            return tmp;
        }

        /// <summary>
        /// 获取数组中X方向距离_origin最远的点
        /// </summary>
        /// <param name="_array"></param>
        /// <param name="_origin"></param>
        /// <returns></returns>
        public static float GetMaxDistanceXInArrayVector3(Vector3[] _array,Vector3 _origin)
        {
            float max = 0f;
            float a = 0f;
            for(int i=0;i<_array.Length;i++)
            {
                a = Mathf.Abs(_origin.x - _array[i].x);
                if (a>max)
                {
                    max = a;
                }
            }

            return max;
        }

        /// <summary>
        /// 获取数组中Z方向距离_origin最远的点
        /// </summary>
        /// <param name="_array"></param>
        /// <param name="_origin"></param>
        /// <returns></returns>
        public static float GetMaxDistanceZinArrayVector3(Vector3[] _array,Vector3 _origin)
        {
            float max = 0f;
            float a = 0f;
            for (int i = 0; i < _array.Length; i++)
            {
                a = Mathf.Abs(_origin.z - _array[i].z);
                if (a > max)
                {
                    max = a;
                }
            }

            return max;
        }

        /// <summary>
        /// 求和
        /// </summary>
        /// <returns></returns>
        public static float Sum(float[] _group)
        {
            float sum = 0f;

            for(int i=0;i<_group.Length;i++)
            {
                sum += _group[i];
            }

            return sum;
        }

        /// <summary>
        /// 判断是否奇数
        /// </summary>
        public static bool IsOdd(int _number)
        {
            return Convert.ToBoolean(_number & 1);//十进制二进制对应关系：0-00,1-01,2-10,3-11,4-100，奇数最低位永远为1，偶数永远为0
        }

        /// <summary>
        /// 获取浮点数的小数部分
        /// </summary>
        /// <returns></returns>
        public static float GetFractionOfFloat(float _float)
        {
            float tmp = (int)_float;

            return _float - tmp;
        }

        /// <summary>
        /// Vector2Int数据是否包含负值元素
        /// </summary>
        /// <param name="vector2Int"></param>
        /// <returns></returns>
        public static bool IsVector2IntIncludeNegtive(Vector2Int _vector2Int)
        {
            if(_vector2Int.x<0)
            {
                return true;
            }

            if(_vector2Int.y<0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// _values中的值是否按从小到大的顺序与数组成员下标值的顺序对应排列
        /// </summary>
        /// <returns></returns>
        public static bool IsFloatValuesOrdersSmallToBig(float[] _values)
        {
            if (_values == null)
            {
                Debug.LogError("IsFloatValuesOrders - _values==null");

                return false;
            }

            if (_values.Length < 1)
            {
                return false;
            }

            for (int i = 0; i < _values.Length; i++)
            {
                if (i < _values.Length - 1)//最后一个成员不需要再向后比较
                {
                    if (_values[i] >= _values[i + 1])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 获取_value在数值从小到大排列的数组_group中所在范围的下标较小成员的索引
        /// 若_group排列有误或_value不在_group所指定的数值范围内返回-1
        /// 注：若值已经大于数组内的最大值即大于最后一个值则返回数组长度
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_group"></param>
        /// <returns></returns>
        public static int GetValueIndexInSmallToBigFloatRangeGroup(float _value,float[] _group)
        {
            if(!IsFloatValuesOrdersSmallToBig(_group))
            {
                return -1;
            }

            if(_value<=_group[0])
            {
                return 0;
            }

            if(_value>_group[_group.Length-1])
            {
                return _group.Length;
            }

            for(int i=0;i<_group.Length-1;i++)//最大值已经在之前检测完毕
            {
                if((_value>_group[i])&&(_value<=_group[i+1]))
                {
                    return i+1;
                }
            }

            return -1;//无匹配范围
        }
    }
}
