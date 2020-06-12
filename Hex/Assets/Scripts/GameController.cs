using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public enum PlayerTurn
    {
        P1,
        P2,
        P3,
        P4,
        P5,
        P6
    }

    public GameObject hex;

    private List<List<GameObject>> hexes = new List<List<GameObject>>();
    private int radius = 5;
    private float width = 1.05f;
    private float height = 0.91f;
    private int players = 6;
    private PlayerTurn playerTurn = PlayerTurn.P1;

    public GameObject scoring;
    private int p1Score = 0;
    private int p2Score = 0;
    private int p3Score = 0;
    private int p4Score = 0;
    private int p5Score = 0;
    private int p6Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject board = new GameObject("Board");

        for (int y = -radius + 1; y < radius; y++)
        {
            List<GameObject> row = new List<GameObject>();
            float xcap = radius + (radius - 1) - Mathf.Abs(y);
            for (float x = -(xcap - 1) / 2; x < (xcap + 1) / 2; x++)
            {
                GameObject inst = GameObject.Instantiate(hex, new Vector2(x * width, y * height), Quaternion.identity);
                inst.transform.parent = board.transform;
                row.Add(inst);
            }
            hexes.Add(row);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                // bool turn = false;

                ChangeColour(hit.transform.GetComponent<SpriteRenderer>());
                ChangeColour(hit.transform.GetComponent<SpriteRenderer>());

                List<GameObject> adjHexes = GetAdjHex(hit.transform.gameObject);
                foreach (GameObject a in adjHexes)
                {
                    ChangeColour(a.GetComponent<SpriteRenderer>());
                }
            }

            NextTurn();
        }
    }

        private void NextTurn()
    {
        switch (playerTurn)
        {
            case PlayerTurn.P1:
                playerTurn = PlayerTurn.P2;
                scoring.transform.GetChild(0).GetComponent<Text>().color = Color.black;
                scoring.transform.GetChild(1).GetComponent<Text>().color = Color.red;
                break;
            case PlayerTurn.P2:
                if (players == 2)
                {
                    playerTurn = PlayerTurn.P1;
                    scoring.transform.GetChild(1).GetComponent<Text>().color = Color.black;
                    scoring.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                }
                else
                {
                    playerTurn = PlayerTurn.P3;
                    scoring.transform.GetChild(1).GetComponent<Text>().color = Color.black;
                    scoring.transform.GetChild(2).GetComponent<Text>().color = Color.red;
                }
                break;
            case PlayerTurn.P3:
                if (players == 3)
                {
                    playerTurn = PlayerTurn.P1;
                    scoring.transform.GetChild(2).GetComponent<Text>().color = Color.black;
                    scoring.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                }
                else
                {
                    playerTurn = PlayerTurn.P4;
                    scoring.transform.GetChild(2).GetComponent<Text>().color = Color.black;
                    scoring.transform.GetChild(3).GetComponent<Text>().color = Color.red;
                }
                break;
            case PlayerTurn.P4:
                if (players == 4)
                {
                    playerTurn = PlayerTurn.P1;
                    scoring.transform.GetChild(3).GetComponent<Text>().color = Color.black;
                    scoring.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                }
                else
                {
                    playerTurn = PlayerTurn.P5;
                    scoring.transform.GetChild(3).GetComponent<Text>().color = Color.black;
                    scoring.transform.GetChild(4).GetComponent<Text>().color = Color.red;
                }
                break;
            case PlayerTurn.P5:
                if (players == 5)
                {
                    playerTurn = PlayerTurn.P1;
                    scoring.transform.GetChild(4).GetComponent<Text>().color = Color.black;
                    scoring.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                }
                else
                {
                    playerTurn = PlayerTurn.P6;
                    scoring.transform.GetChild(4).GetComponent<Text>().color = Color.black;
                    scoring.transform.GetChild(5).GetComponent<Text>().color = Color.red;
                }
                break;
            case PlayerTurn.P6:
                playerTurn = PlayerTurn.P1;
                scoring.transform.GetChild(5).GetComponent<Text>().color = Color.black;
                scoring.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                break;
        }
    }

    private void ChangeColour(SpriteRenderer hex)
    {
        switch (hex.sprite.name)
        {
            case "HexagonW":
                hex.sprite = Resources.Load<Sprite>("Sprites/HexagonY");
                break;
            case "HexagonY":
                hex.sprite = Resources.Load<Sprite>("Sprites/HexagonO");
                break;
            case "HexagonO":
                hex.sprite = Resources.Load<Sprite>("Sprites/HexagonR");
                break;
            case "HexagonR":
                hex.sprite = Resources.Load<Sprite>("Sprites/HexagonP");
                break;
            case "HexagonP":
                hex.sprite = Resources.Load<Sprite>("Sprites/HexagonB");
                break;
            case "HexagonB":
                hex.sprite = Resources.Load<Sprite>("Sprites/HexagonG");
                switch (playerTurn)
                {
                    case PlayerTurn.P1:
                        hex.gameObject.GetComponentInChildren<Text>().enabled = true;
                        p1Score++;
                        scoring.transform.GetChild(0).GetComponent<Text>().text = "P1: " + p1Score;
                        break;
                    case PlayerTurn.P2:
                        hex.gameObject.GetComponentInChildren<Text>().enabled = true;
                        hex.gameObject.GetComponentInChildren<Text>().text = "P2";
                        p2Score++;
                        scoring.transform.GetChild(1).GetComponent<Text>().text = "P2: " + p2Score;
                        break;
                    case PlayerTurn.P3:
                        hex.gameObject.GetComponentInChildren<Text>().enabled = true;
                        hex.gameObject.GetComponentInChildren<Text>().text = "P3";
                        p3Score++;
                        scoring.transform.GetChild(2).GetComponent<Text>().text = "P3: " + p3Score;
                        break;
                    case PlayerTurn.P4:
                        hex.gameObject.GetComponentInChildren<Text>().enabled = true;
                        hex.gameObject.GetComponentInChildren<Text>().text = "P4";
                        p4Score++;
                        scoring.transform.GetChild(3).GetComponent<Text>().text = "P4: " + p4Score;
                        break;
                    case PlayerTurn.P5:
                        hex.gameObject.GetComponentInChildren<Text>().enabled = true;
                        hex.gameObject.GetComponentInChildren<Text>().text = "P5";
                        p5Score++;
                        scoring.transform.GetChild(4).GetComponent<Text>().text = "P5: " + p5Score;
                        break;
                    case PlayerTurn.P6:
                        hex.gameObject.GetComponentInChildren<Text>().enabled = true;
                        hex.gameObject.GetComponentInChildren<Text>().text = "P6";
                        p6Score++;
                        scoring.transform.GetChild(5).GetComponent<Text>().text = "P6: " + p6Score;
                        break;
                }
                break;
        }
    }

    private List<GameObject> GetAdjHex(GameObject hex)
    {
        List<GameObject> adjHexes = new List<GameObject>();

        RaycastHit2D hit = Physics2D.Raycast(hex.transform.position + new Vector3(1.05f, 0f), Vector2.zero);

        if (hit.collider != null)
        {
            adjHexes.Add(hit.transform.gameObject);
        }

        hit = Physics2D.Raycast(hex.transform.position + new Vector3(-1.05f, 0f), Vector2.zero);

        if (hit.collider != null)
        {
            adjHexes.Add(hit.transform.gameObject);
        }

        hit = Physics2D.Raycast(hex.transform.position + new Vector3(0.525f, 0.91f), Vector2.zero);

        if (hit.collider != null)
        {
            adjHexes.Add(hit.transform.gameObject);
        }

        hit = Physics2D.Raycast(hex.transform.position + new Vector3(-0.525f, 0.91f), Vector2.zero);

        if (hit.collider != null)
        {
            adjHexes.Add(hit.transform.gameObject);
        }

        hit = Physics2D.Raycast(hex.transform.position + new Vector3(0.525f, -0.91f), Vector2.zero);

        if (hit.collider != null)
        {
            adjHexes.Add(hit.transform.gameObject);
        }

        hit = Physics2D.Raycast(hex.transform.position + new Vector3(-0.525f, -0.91f), Vector2.zero);

        if (hit.collider != null)
        {
            adjHexes.Add(hit.transform.gameObject);
        }

        return adjHexes;
    }

}
