﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    [SerializeField]
    private Sprite rock, paper, scissors;

    [SerializeField]
    private Image p1_choice_img, p2_choice_img;

    [SerializeField]
    private Text info;

    [SerializeField]
    private Text p1_score_text, p2_score_text;

    private string p1_choice = "", p2_choice = "";

    private AnimationController animationController;

    private int max_score = 3;

    private int p1_id, p2_id;
    private int p1_score = 0, p2_score = 0;

    private NetworkManager networkManager;

    public Player[] Players = new Player[2];

    private int currentPlayer = 1;

    void Start()
    {
        // DontDestroyOnLoad(gameObject);
        networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
        MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();
        msgQueue.AddCallback(Constants.SMSG_MOVE, OnResponseMove);
    }

    public Player GetCurrentPlayer()
	{
		return Players[currentPlayer - 1];
	}

    void Awake() {
        animationController = GetComponent<AnimationController>();
    }

    public void Init(Player player1, Player player2)
    {
        Players[0] = player1;
        Players[1] = player2;
        currentPlayer = 1;
    }

    public void SetChoices(string gameChoices) {
 
        switch(gameChoices) {
            case "Rock":
                p1_choice_img.sprite = rock;
                p1_choice = "Rock";
                break;
            case "Paper":
                p1_choice_img.sprite = paper;
                p1_choice = "Paper";
                break;
            case "Scissors":
                p1_choice_img.sprite = scissors;
                p1_choice = "Scissors";
                break;
        }
        
        //if p2 made choice first, call play and send results
        networkManager.SendMoveRequest(currentPlayer, p1_choice);
    }

    IEnumerator DisplayWinnerAndMoveOn(int player) {
        yield return new WaitForSeconds(2f);
        info.gameObject.SetActive(true);
        if (player == 1) {
            p1_score++;
            p1_score_text.text = "Your score: " + p1_score;
        }
        else if (player == 2) {
            p2_score++;
            p2_score_text.text = "Their score: " + p2_score;
        }
        if(p1_score == max_score){
            //player 1 wins
            info.text = "You are Victorious!";
        }
        else if(p2_score == max_score){
            //player 2 wins
            info.text = "You are the Loser...";
        }
        else {
            yield return new WaitForSeconds(2f);
            info.gameObject.SetActive(false);
            animationController.ResetAnimations();
        }
    }

    public void OnResponseMove(ExtendedEventArgs eventArgs) {
        if (p1_choice != "")
            animationController.PlayerMadeChoice();
    }
}
