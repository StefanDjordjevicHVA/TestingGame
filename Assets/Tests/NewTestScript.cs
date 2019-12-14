﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    

    public class NewTestScript
    {
        [UnityTest]
        public IEnumerator PlayerMovementTestingLeft()
        {
            //Test to check that player cant move to the left more then is alowed.
            GameObject player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("ParentPlayer"));

            PlayerController controller = player.GetComponentInChildren<PlayerController>();

            controller.MovePlayer(true, false, false);

            //wait for 1 second so that it will use several frames.
            yield return new WaitForSeconds(1f);

            Assert.AreEqual(0, controller.GetComponentInChildren<PlayerController>().currentPos);
            
        }

        [UnityTest]
        public IEnumerator PlayerMovementTestingRight()
        {
            //Test to check that player cant move to the right more then is alowed.
            GameObject player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("ParentPlayer"));

            PlayerController controller = player.GetComponentInChildren<PlayerController>();

            controller.MovePlayer(false, true, false);

            //wait for 1 second so that it will use several frames.
            yield return new WaitForSeconds(1f);

            Assert.AreEqual(2, controller.GetComponentInChildren<PlayerController>().currentPos);

        }
    }
}