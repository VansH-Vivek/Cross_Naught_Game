using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public  int[] placements = new int[9];

    public GameObject noughtPrefab;
    public GameObject crossPrefab;
    public TextMeshProUGUI currentPlayerTxt;
    public TextMeshProUGUI WinText;
    public GameObject winPanel;
    public int currentPlayer;
    GameObject currentPlayerTurnPrefab;

    private void Start()
    {
        winPanel.SetActive(false);

        currentPlayer = 1;
        string playername = "Naught";
        if (currentPlayer == 2)
        {
            playername = "Cross";
        }
        currentPlayerTxt.text = "Current Turn:\n<b>" + playername + "</b>";
      
        currentPlayerTurnPrefab = noughtPrefab;
        
        for (int i = 0; i < placements.Length; i++)
        {
            placements[i] = 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        bool clickedTile = false;
        GameObject _selectedTile = null;

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);
            
            if(hit.collider != null)
            {
                if(hit.collider.name.Contains("Tile"))
                {
                    clickedTile = true;
                    _selectedTile = hit.collider.gameObject;
                }
            }
        }

        if (clickedTile)
        {
            GameObject _newPrefab = GameObject.Instantiate(currentPlayerTurnPrefab);
            _newPrefab.transform.position = _selectedTile.transform.position;

            string name = _selectedTile.name;
            name = name.Replace("Tile","");
            int num = int.Parse(name);
            placements[num-1] = currentPlayer;
;            
            Destroy(_selectedTile);

            ChangeTurn();
            
            if(CheckWin(1))
            {
                winPanel.SetActive(true);
                WinText.text = "Naught Won!";
            }
            else if(CheckWin(2))
            {
                winPanel.SetActive(true);
                WinText.text = "Cross Won!";
            }
        }
    }
    bool CheckWin(int playerCheck)
    {
        if(placements[0] == playerCheck && placements[1] == playerCheck && placements[2] == playerCheck)
        {
            return true;
        }
        if (placements[3] == playerCheck && placements[4] == playerCheck && placements[5] == playerCheck)
        {
            return true;
        }
        if (placements[6] == playerCheck && placements[7] == playerCheck && placements[8] == playerCheck)
        {
            return true;
        }

        if (placements[0] == playerCheck && placements[3] == playerCheck && placements[6] == playerCheck)
        {
            return true;
        }
        if (placements[1] == playerCheck && placements[4] == playerCheck && placements[7] == playerCheck)
        {
            return true;
        }
        if (placements[2] == playerCheck && placements[5] == playerCheck && placements[8] == playerCheck)
        {
            return true;
        }

        //diagonal
        if (placements[0] == playerCheck && placements[4] == playerCheck && placements[8] == playerCheck)
        {
            return true;
        }
        if (placements[2] == playerCheck && placements[4] == playerCheck && placements[6] == playerCheck)
        {
            return true;
        }
        if (placements[2] == playerCheck && placements[1] == playerCheck && placements[2] == playerCheck)
        {
            return true;
        }
        return false;
    }
    void ChangeTurn()
    {
        if (currentPlayerTurnPrefab.name == noughtPrefab.name)
        {
            currentPlayerTurnPrefab = crossPrefab;
            currentPlayer = 2;
        }
        else
        {
            currentPlayerTurnPrefab = noughtPrefab;
            currentPlayer = 1;
        }


        string playername = "Naught";
        if (currentPlayer == 2)
        {
            playername = "Cross";
        }
        currentPlayerTxt.text = "Current Turn:\n<b>" + playername + "</b>";
    }
    public void RestartButtonPresses()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
