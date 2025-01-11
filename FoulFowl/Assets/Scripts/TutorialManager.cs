using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public List<TextMeshProUGUI> tutorialTextList;
    public List<Image> tutorialImageList;

    public AudioSource BGMusic;

    // Start is called before the first frame update
    void Start()
    {
        BGMusic.time = MusicData.currentTime;

        for (int i = 0; i < 3; i++)
        {
            TextMeshProUGUI text = tutorialTextList[i];
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

            Image img = tutorialImageList[i];
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
        }

        StartCoroutine("TutorialFade");
    }

    // Update is called once per frame
    void Update()
    {
        MusicData.currentTime = BGMusic.time;
    }

    public IEnumerator TutorialFade()
    {
        for (int i = 0; i < tutorialTextList.Count; i++)
        {
            for (float alpha = 0f; alpha <= 1; alpha += 0.025f)
            {
                TextMeshProUGUI text = tutorialTextList[i];
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

                Image img = tutorialImageList[i];
                img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public void ContinuePressed()
    {
        SceneManager.LoadScene("PlayerSelectScene");
    }

    public void BackPressed()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
