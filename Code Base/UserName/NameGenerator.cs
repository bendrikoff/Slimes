using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NameGenerator : Singleton<NameGenerator>
{
   public List<CountriesFlag> CountriesFlags;
   
   public List<UserName> Names;

   private Queue<UserName> _names;

   public UserName GetName()
   {
      _names = new Queue<UserName>();
      ShuffleList(Names);
      foreach (var name in Names)
      {
         _names.Enqueue(name);
      }

      return _names.Dequeue();
   }
   
   public void ShuffleList(List<UserName> list)
   {
      var rng = new Random();
      int n = list.Count;
      while (n > 1)
      {
         n--;
         int k = rng.Next(n + 1);
         (list[k], list[n]) = (list[n], list[k]);
      }
   }
}