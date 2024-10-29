using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

// 存放人物的总数据
public class Characters
{
    private static Character[] leftCharacterList;
    private static Character[] rightCharacterList;
    public static ArrayList characterList;
    private static int leftPriestCount = 0;
    private static int rightPriestCount = 0;
    private static int leftDevilCount = 0;
    private static int rightDevilCount = 0;
    public Characters(GameObject priest, GameObject devil)
    {
        leftCharacterList = new Character[6];
        rightCharacterList = new Character[6];
        characterList = new ArrayList();
        Character priest1 = new Character(priest,0);        
        Character priest2 = new Character(priest, 1);
        Character priest3 = new Character(priest, 2);
        Character devil1 = new Character(devil, 3, false);
        Character devil2 = new Character(devil, 4, false);
        Character devil3 = new Character(devil, 5, false);
        
        characterList.Add(priest1);
        characterList.Add(priest2);
        characterList.Add(priest3);
        characterList.Add(devil1);
        characterList.Add(devil2);
        characterList.Add(devil3);
        leftCharacterList[0] = priest1;
        leftCharacterList[1] = priest2;
        leftCharacterList[2] = priest3;
        leftCharacterList[3] = devil1;
        leftCharacterList[4] = devil2;
        leftCharacterList[5] = devil3;

        leftPriestCount = 3;
        rightPriestCount = 0;
        leftDevilCount = 3;
        rightDevilCount = 0;
}

    public static bool isLose()
    {
        if (leftPriestCount > 0 && leftPriestCount < leftDevilCount)
        {
            foreach (Character character in leftCharacterList) {
                if(character != null && character.isPriest)
                {
                    character.gameobject.transform.eulerAngles = new Vector3(0, 180, 90);
                    Vector3 origin = character.gameobject.transform.position;
                    character.gameobject.transform.position = new Vector3(origin[0], origin[1]+0.07f, origin[2]);
                }
            }
            return true;
        }
        if (rightPriestCount > 0 && rightPriestCount < rightDevilCount)
        {
            foreach (Character character in rightCharacterList)
            {
                if (character != null && character.isPriest)
                {
                    character.gameobject.transform.eulerAngles = new Vector3(0, 0, 90);
                    Vector3 origin = character.gameobject.transform.position;
                    character.gameobject.transform.position = new Vector3(origin[0], origin[1] + 0.07f, origin[2]);
                }
            }
            return true;
        }
        return false;
    }

    public static bool isWin()
    {
        if(rightPriestCount + rightDevilCount == 6)
        {
            return true;
        }
        return false;
    }

    // 将人物在岸上和船上移动
    public bool moveCharacter22Boat(Character character, Boat boat, out Vector3 targetPosition)
    {
        targetPosition = Vector3.zero;
        // 从船上到岸上
        if (character.isInBoat)
        {
            boat.outPerson(character);  // 告知船有人离开
            int i = 0;
            if (character.isLeft)
            {
                foreach(Character charac in leftCharacterList)
                {
                    if(charac == null)
                    {
                        break;
                    }
                    i++;
                }
                leftCharacterList[i]=character;
                targetPosition = Position.leftPosition[i];
                if (character.isPriest)
                {
                    leftPriestCount++;
                }
                else
                {
                    leftDevilCount++;
                }
            }
            else
            {
                foreach (Character charac in rightCharacterList)
                {
                    if (charac == null)
                    {
                        break;
                    }
                    i++;
                }
                rightCharacterList[i] = character;
                targetPosition = Position.rightPosition[i];
                if (character.isPriest)
                {
                    rightPriestCount++;
                }
                else
                {
                    rightDevilCount++;
                }
            }
            character.isInBoat = false;
            
        }
        else  // 从岸上到船上
        {
            int i = 0;
            if (character.isLeft)
            {
                if (boat.isLeft && boat.loadPerson(character, out targetPosition))  // 询问船是否能载人
                {
                    foreach (Character charac in leftCharacterList)
                    {
                        if(charac != null && charac.id == character.id)
                        {
                            leftCharacterList[i] = null;
                        }
                        i++;
                    }
                    if (character.isPriest)
                    {
                        leftPriestCount--;
                    }
                    else
                    {
                        leftDevilCount--;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if ((!boat.isLeft) && boat.loadPerson(character, out targetPosition))  // 询问船是否能载人
                {
                    foreach (Character charac in rightCharacterList)
                    {
                        if (charac != null && charac.id == character.id)
                        {
                            rightCharacterList[i] = null;
                        }
                        i++;
                    }
                    if (character.isPriest)
                    {
                        rightPriestCount--;
                    }
                    else
                    {
                        rightDevilCount--;
                    }
                }
                else
                {
                    return false;
                }
            }
            character.isInBoat = true;
        }
        return true;
    }

}
