using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Linq;
public class Window_Graph : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    int colorNum;
    
    private void Awake()
    {
        string Username = GameObject.FindWithTag("MainData").GetComponent<MainData>().personal_id;
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        //CreateCircle(new Vector2(200, 200));
        List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33, 28 };
        //ShowGraph(valueList);
        //string Username = "eunjju1";
        print("linegraph user name" + Username);
        string[,] readDatas = ReadCsv("C:/Users/DS/Desktop/medidapipe/"+ Username+".csv");
        int num = readDatas.GetLength(1);
        List<int> numList = new List<int>();
        List<int> numListtheta = new List<int>();
        List<int> numListAlpha = new List<int>();
        List<int> numListBeta = new List<int>();
        List<int> numListGamma = new List<int>();

        for (int i = 0; i < num - 3; i++)
        {
            int temp1 = int.Parse(readDatas[0, i]);
            //print(temp1);
            int temp2 = int.Parse(readDatas[1, i]);
            int temp4 = int.Parse(readDatas[3, i]);
            int temp6 = int.Parse(readDatas[5, i]);
            int temp8 = int.Parse(readDatas[7, i]);
            //print(temp);
            numList.Add(temp1);
            numListtheta.Add(temp2);
            numListAlpha.Add(temp4);
            numListBeta.Add(temp6);
            numListGamma.Add(temp8);
        }
        //print(numList[0]);
        ShowGraph(numList, 0);
        ShowGraph(numListtheta, 1);
        ShowGraph(numListAlpha, 2);
        ShowGraph(numListBeta, 3);
        ShowGraph(numListGamma, 4);

        //print(numList[1]);
        //print(readDatas[1, 1].GetType());
    }
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(0, 0);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList, int num)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 50000f;
        float xSize = 14f;
        
        colorNum = num;
        if (colorNum > 1)
        {
            yMaximum = 30000f;
        }
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            //float yPosition = (valueList[i] / yMaximum) * graphHeight;
            float yPosition = (valueList[i] / yMaximum) * 4.5f;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));

            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, colorNum);
            }

            lastCircleGameObject = circleGameObject;
        }
    }
    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, int colorNum)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        if (colorNum == 0)
        {
            //gameObject.GetComponent<Image>().color = new Color(237/255f, 238 / 255f, 243 / 255f, 1);
            gameObject.GetComponent<Image>().color = new Color(216 / 255f, 140 / 255f, 154 / 255f, 1); //216, 140, 154)
        }
        else if (colorNum == 1)
        {
            gameObject.GetComponent<Image>().color = new Color(242 / 255f, 208 / 255f, 169 / 255f, 1); //(242, 208, 169)
        }
        else if (colorNum == 2)
        {
            gameObject.GetComponent<Image>().color = new Color(241 / 255f, 227 / 255f, 211 / 255f, 1); //rgb(241, 227, 211)
        }
        else if (colorNum == 3)
        {
            gameObject.GetComponent<Image>().color = new Color(153 / 255f, 193 / 255f, 185 / 255f, 1); //rgb(153, 193, 185)
        }
        else if (colorNum == 4)
        {
            gameObject.GetComponent<Image>().color = new Color(142 / 255f, 125 / 255f, 148 / 255f, 1);//rgb(142, 125, 190)
        }
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 6f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));

    }
    public string[,] ReadCsv(string filePath)
    {
        string value = "";
        StreamReader reader = new StreamReader(filePath, Encoding.UTF8);
        value = reader.ReadToEnd();
        reader.Close();

        string[] lines = value.Split("\n"[0]);

        int width = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            width = Mathf.Max(width, row.Length);
        }

        string[,] outputGrid = new string[width + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];
                outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
            }
        }

        return outputGrid;
    }

    public string[] SplitCsvLine(string line)
    {
        return (from Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }
}