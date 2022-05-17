﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NyarlaEssentials
{
    public static class NECollections
    {
        public static List<T> ChooseRandomElements<T>(this List<T> collection, int ammount)
        {
            List<T> choosed = new List<T>();
            for (int i = 0; i < ammount; i++)
            {
                if (collection.Count > 0)
                {
                    T element = collection[Random.Range(0, collection.Count)];
                    choosed.Add(element);
                    collection.Remove(element);
                }
                else
                {
                    break;
                }
            }
            return choosed;
        }
        
        public static List<T> TakeAwayRandomElements<T>(ref List<T> collection, int ammount)
        {
            List<T> choosed = new List<T>();
            for (int i = 0; i < ammount; i++)
            {
                if (collection.Count > 0)
                {
                    T element = collection[Random.Range(0, collection.Count)];
                    choosed.Add(element);
                    collection.Remove(element);
                }
                else
                {
                    break;
                }
            }
            return choosed;
        }

        public static T ChooseRandomElement<T>(this List<T> collection)
        {
            if (collection.Count <= 0)
                throw new IndexOutOfRangeException();
            return collection[Random.Range(0, collection.Count)];
        }

        public static string CollectionToString<T>(T[] collection)
        {
            string result = "";
            for (int i = 0; i < collection.Length; i++)
            {
                result += collection[i].ToString();
                if (i < collection.Length - 1)
                    result += ",\n";
            }
            return result;
        }

        public static T[] CreateArrayOfContent<T>(int length, T content)
        {
            T[] collection = new T[length];
            for (int i = 0; i < collection.Length; i++)
            {
                collection[i] = content;
            }
            return collection;
        }

        public static T[] Copy<T>(this T[] originCollection)
        {
            T[] finalCollection = new T[originCollection.Length];
            for (int i = 0; i < finalCollection.Length; i++)
            {
                finalCollection[i] = originCollection[i];
            }
            return finalCollection;
        }
        
        public static T[] TakeRange<T>(this IEnumerable<T> collection, int from /*inclusive*/, int to /*exclusive*/)
        {
            T[] array = collection.ToArray();
            if (from < 0 || from > array.Length || to < 0 || to > array.Length)
            {
                throw new IndexOutOfRangeException();
            }
            if (from > to)
            {
                throw new Exception("'from' argument must lesser or equal then 'to' argument");
            }

            T[] final = new T[to - from];
            for (int i = from; i < to; i++)
            {
                final[i - from] = array[i];
            }
            return final;
        }

        public static bool HasIndex<T>(this IEnumerable<T> collection, int index) => index < collection.ToArray().Length;

        public static T[] Shuffle<T>(this List<T> collection) => collection.OrderBy(t => Random.value).ToArray();
    }
}
