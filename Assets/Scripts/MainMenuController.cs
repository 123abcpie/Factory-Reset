using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
   public void PlaySingleplayer () 
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void PlayMultiplayer ()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
   }
}
