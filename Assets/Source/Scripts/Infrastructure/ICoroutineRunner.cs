﻿using System.Collections;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}