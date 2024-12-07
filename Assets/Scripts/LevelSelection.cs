//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class LevelSelection : MonoBehaviour
//{
//    public Button[] Button;
//    public Image unlockimage;
//    public GameObject BackButton;
//    public GameObject MainMenu;
//    public GameObject Levelselection;
//    // Start is called before the first frame update
//    void Start()
//    {
//        int level = SaveSystem.Load("currentlevel", 0);
//        SaveSystem.Save("level0unlock", true);
//        for (int i = 0; i < Button.Length; i++)
//        {
//            bool unlock = SaveSystem.Load("level" + i + "unlock", false);
//            if (!unlock)
//            {
//                Button[i].interactable = false;
//                if (Button[i].transform.childCount > 1)
//                {
//                    Button[i].transform.GetChild(0).gameObject.SetActive(false);
//                }
//            }
//            else
//            {
//                Button[i].interactable = true;
//                if (Button[i].transform.childCount > 1)
//                {
//                    Button[i].transform.GetChild(0).gameObject.SetActive(true);
//                    Button[i].transform.GetChild(1).gameObject.SetActive(false);
//                }
//            }
//        }
//    }
//    public void StartLevel(int level)
//    {
//        SaveSystem.Save("currentlevel", level);
//        FindObjectOfType<UIManager>().RetryButton();
//    }
//    public void backbutton()
//    {
//        BackButton.SetActive(true);
//        MainMenu.SetActive(true);
//        Levelselection.SetActive(false);

//    }
//}