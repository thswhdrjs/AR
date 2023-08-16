using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ispark
{
    public class GLTFController : MonoBehaviour
    {
        public GameObject[] gltfs;

        //private void Start()
        //{
        //    StartCoroutine(GLTFPlayer());
        //}

        void OnEnable()
        {
            StartCoroutine(GLTFPlayer());
        }

        IEnumerator GLTFPlayer()
        {
            foreach (GameObject num in gltfs)
            {
                for (int i = 0; i < gltfs.Length; i++)
                {
                    gltfs[i].SetActive(false);
                }

                num.SetActive(true);
                yield return new WaitForSeconds(0.015f);
            }

        }
    }
}