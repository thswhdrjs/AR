using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ispark
{
    public class GLTFController : MonoBehaviour
    {
        public GameObject[] gltfs;
        private int count;

        void OnEnable()
        {
            StartCoroutine(GLTFPlayer());
        }

        IEnumerator GLTFPlayer()
        {
            foreach (GameObject num in gltfs)
            {
                num.SetActive(true);

                for (int i = 0; i < gltfs.Length; i++)
                {
                    if (gltfs[i] != num)
                        gltfs[i].SetActive(false);
                }

                if (num == gltfs[gltfs.Length - 45])
                    break;
                else
                    yield return new WaitForSeconds(0.015f);
            }

            if(transform.eulerAngles.y == 180)
                transform.position += new Vector3(0, 0, 3000);
            else
                transform.position += new Vector3(-3000, 0, 0);

            count++;
                
            StartCoroutine(GLTFPlayer());
        }

        private void Update()
        {
            if (count > 1)
            {
                if (transform.eulerAngles.y < 90)
                    transform.eulerAngles = new Vector3(0, 90, 0);
                else
                    transform.eulerAngles += new Vector3(0, -Time.deltaTime * 100f, 0);
            }
        }
    }
}