using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class PresentationController : MonoBehaviour
{
    private const int LAST_SESSION_INDEX = 7;

    public Text titleText;

    public Text timeText;

    public Text locationText;

    public Text presenterText;

    public Text presenterText2;

    public GameObject mainScreenPresenter;

    public GameObject presenterVideo;

    public GameObject presenterPicture;

    public GameObject presenterPicture2;

    public GameObject presenterTitle2;

    public VideoClip hackADroneVideo;

    public VideoClip royVideo;

    public VideoClip bertjanVideo;

    public VideoClip brianVideo;

    public VideoClip richardAbbuhlVideo;

    public VideoClip thomasVideo;

    public VideoClip peterVideo;

    public Material ordinaPhoto;

    public Material richardAbbuhlPhoto;

    public Material hannoEmbregtsPhoto;

    public Material nsemekeUkpongPhoto;

    public Material martinFortschPhoto;

    public Material thomasEndresPhoto;

    public AudioClip hannoAudio;

    public AudioClip nsemekeAudio;
    
    private double slideInterval;

    private float startTimeSlideShow;

    private Texture2D[] slideTextures;

    private int currentSlideIndex;

    private int presentationIndex = -1;

    private AudioSource mainScreenAudioSource;

    private VideoPlayer mainScreenVideoPlayer;

    private Renderer mainScreenRenderer;

    private void Start()
    {
        Play();
    }

    private void OnEnable()
    {
        mainScreenAudioSource = mainScreenPresenter.GetComponent<AudioSource>();
        mainScreenVideoPlayer = mainScreenPresenter.GetComponent<VideoPlayer>();
        mainScreenRenderer = mainScreenPresenter.GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        AdvanceSlideShow();
    }

    public void Previous()
    {
        presentationIndex--;
        if (presentationIndex < 0)
        {
            presentationIndex = LAST_SESSION_INDEX;
        }
        Play();
    }

    public void Next()
    {
        presentationIndex++;
        if (presentationIndex > LAST_SESSION_INDEX)
        {
            presentationIndex = 0;
        }
        Play();
    }

    private void Play()
    {
        ResetStage();
        switch (presentationIndex)
        {
            case -1:
                titleText.text = "Opening J-Fall 2017";
                timeText.text = "09:00 - 09:20";
                locationText.text = "Expo Theater";
                presenterText.text = "Bert Jan Schrijver";
                SetMainScreenSlideShow("Slides/intro", bertjanVideo.length);
                SetPresenterVideo(bertjanVideo);
                break;
            case 0:
                titleText.text = "Identity theft: Developers are key";
                timeText.text = "08:00 - 08:50";
                locationText.text = "Zaal 7";
                presenterText.text = "Brian Vermeer";
                SetMainScreenSlideShow("Slides/brian", brianVideo.length);
                SetPresenterVideo(brianVideo);
                break;
            case 1:
                titleText.text = "Fostering an evolving architecture in the agile world";
                timeText.text = "11:40 - 12:30";
                locationText.text = "Expo Theater";
                presenterText.text = "Roy van Rijn";
                SetMainScreenSlideShow("Slides/roy", royVideo.length);
                SetPresenterVideo(royVideo);
                break;
            case 2:
                titleText.text = "AI self-learning game playing";
                timeText.text = "14:35 - 15:25";
                locationText.text = "Zaal 6";
                presenterText.text = "Richard Abbuhl";
                SetPresenterPicture(richardAbbuhlPhoto);
                SetMainScreenVideo(richardAbbuhlVideo);
                break;
            case 3:
                titleText.text = "Project Avatar - Telepresence robotics with NAO and Kinect";
                timeText.text = "14:35 - 15:25";
                locationText.text = "Zaal 7";
                presenterText.text = "Martin Förtsch";
                presenterText2.text = "Thomas Endres";
                SetPresenterPicture(martinFortschPhoto);
                SetPresenterPicture2(thomasEndresPhoto);
                SetMainScreenVideo(thomasVideo);
                break;
            case 4:
                titleText.text = "Hack A Drone!";
                timeText.text = "14:35 - 17:40";
                locationText.text = "Hands-on Labs 2";
                presenterText.text = "Ordina team";
                SetPresenterPicture(ordinaPhoto);
                SetMainScreenVideo(hackADroneVideo);
                break;
            case 5:
                titleText.text = "Configuring Kafka Producers for Stream Processing";
                timeText.text = "15:45 - 16:35";
                locationText.text = "Zaal 3";
                presenterText.text = "Nsemeke Ukpong";
                SetPresenterPicture(nsemekeUkpongPhoto);
                SetMainScreenSlideShow("Slides/nsemeke", nsemekeAudio.length);
                SetMainScreenAudio(nsemekeAudio);
                break;
            case 6:
                titleText.text = "Building a Spring Boot application: Ask the Audience!";
                timeText.text = "16:50 - 17:40";
                locationText.text = "Zaal 3";
                presenterText.text = "Hanno Embregts";
                SetPresenterPicture(hannoEmbregtsPhoto);
                SetMainScreenSlideShow("Slides/hanno", nsemekeAudio.length);
                SetMainScreenAudio(hannoAudio);
                break;
            case 7:
                titleText.text = "VR/AR and Java: developing the J-Fall VR app";
                timeText.text = "16:50 - 17:40";
                locationText.text = "Zaal 4";
                presenterText.text = "Peter Hendriks";
                SetMainScreenSlideShow("Slides/peter", peterVideo.length);
                SetPresenterVideo(peterVideo);
                break;
        }
    }

    private void ResetStage()
    {
        UnloadSlideShow();
        presenterPicture.SetActive(false);
        presenterPicture2.SetActive(false);
        presenterTitle2.SetActive(false);
        presenterVideo.SetActive(false);
        mainScreenVideoPlayer.enabled = false;
        mainScreenAudioSource.enabled = false;
        mainScreenAudioSource.clip = null;
    }

    private void SetMainScreenVideo(VideoClip videoClip)
    {
        mainScreenVideoPlayer.enabled = true;
        mainScreenAudioSource.enabled = true;
        mainScreenVideoPlayer.clip = videoClip;
    }

    private void SetMainScreenSlideShow(string resourcePath, double time)
    {        
        slideTextures = Resources.LoadAll<Texture2D>(resourcePath);
        slideInterval = time / slideTextures.Length;

        ResetSlides();
        PresentCurrentSlide();
    }

    private void SetMainScreenAudio(AudioClip audioClip)
    {
        mainScreenAudioSource.clip = audioClip;
        mainScreenAudioSource.enabled = true;
    }

    private void SetPresenterVideo(VideoClip clip)
    {
        VideoPlayer videoPlayer = presenterVideo.GetComponent<VideoPlayer>();
        videoPlayer.clip = clip;
        videoPlayer.skipOnDrop = true;
        presenterVideo.SetActive(true);
    }
    
    private void SetPresenterPicture(Material picture)
    {
        presenterPicture.SetActive(true);
        presenterPicture.GetComponent<Renderer>().material = picture;
    }

    private void SetPresenterPicture2(Material picture)
    {
        presenterPicture2.SetActive(true);
        presenterTitle2.SetActive(true);
        presenterPicture2.GetComponent<Renderer>().material = picture;
    }

    private void AdvanceSlideShow()
    {
        if (slideTextures == null)
        {
            return;
        }
        if (Time.time - startTimeSlideShow >= (slideInterval * (currentSlideIndex + 1)))
        {
            currentSlideIndex++;
            if (currentSlideIndex == slideTextures.Length)
            {
                ResetSlides();
            }
            PresentCurrentSlide();
        }
    }

    private void PresentCurrentSlide()
    {
        mainScreenRenderer.material.mainTexture = slideTextures[currentSlideIndex];
    }

    private void ResetSlides()
    {
        currentSlideIndex = 0;
        startTimeSlideShow = Time.time;
    }

    private void UnloadSlideShow()
    {
        if (slideTextures != null)
        {
            foreach (var slide in slideTextures)
            {
                Resources.UnloadAsset(slide);
            }
        }
        slideTextures = null;
    }
}
