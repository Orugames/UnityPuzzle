using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSolver : MonoBehaviour
{
    public LevelEditor levelEditor;
    public List<int> numbersPossible = new List<int>() { 0, 0, 0 };
    public Stack<int> stack1 = new Stack<int>();
    public Stack<int> stack2 = new Stack<int>();
    public Stack<int> stack3 = new Stack<int>();

    public Cube cube1;
    public Cube cube2;
    public List<Cube> cubes = new List<Cube>();

    public bool isComplete;
    public bool validNumber;
    public bool validCombination;
    public bool start;

    public int input1;
    public int input2;
    public int input3;

    public void StartSolution()
    {
        start = false;
        input1 = 0;
        input2 = 0;
        input3 = 0;
        StartCoroutine(SearchRoutine());
    }
    public void Update()
    {
        if (levelEditor.cubes.Count > 1)
        {
            cube1 = levelEditor.cubes[0];
            cube2 = levelEditor.cubes[1];
        }
        if (start)
        {
            StartSolution();
        }
    }
    public IEnumerator SearchRoutine(float timeStep = 0.1f)
    {
        Debug.Log("Entering Coroutine");

        cube2.side1Solver.number = 1;
        cube2.side1Solver.oposedSide.number = 7 - 1;
        cube2.side2Solver.number = 2;
        cube2.side2Solver.oposedSide.number = 7 - 3;
        cube2.side3Solver.number = 3;
        cube2.side3Solver.oposedSide.number = 7 - 3;

        for (int x = 0; x < 2; x++)
        {
            if (x == 0) //turn for the other cube to solve
            {
                FillCube(cubes[0], 0);
            }
            for (int i = 1; i < 7; i++)
            {
                stack1.Push(i);
                input1 = i;
                cube1.side1Solver.number = i;
                cube1.side1Solver.oposedSide.number = 7 - i;
                for (int j = 1; j < 7; j++)
                {
                    bool input2Validity = checkIfInputValid(j, i);

                    if (input2Validity)
                    {
                        stack2.Push(j);
                        input2 = j;
                        cube1.side2Solver.number = j;
                        cube1.side2Solver.oposedSide.number = 7 - j;
                    }
                    for (int k = 1; k < 7; k++)
                    {
                        bool input3Validity = checkIfInputValid(k, i, j);
                        if (input3Validity)
                        {
                            stack3.Push(k);
                            input3 = k;
                            cube1.side3Solver.number = k;
                            cube1.side3Solver.oposedSide.number = 7 - k;
                            Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3);
                            //Introducir dat
                            //comprobar si es correcto
                            cube1.UpdateCube();
                            cube2.UpdateCube();
                            if (cube1.cubeCompleted && cube2.cubeCompleted)
                            {
                                Debug.Log("Posible Solucion, salvamos");
                                yield break;
                            }

                        }
                    }
                    yield return new WaitForEndOfFrame();
                }
            }
        }

    }


    public bool FillCube(Cube cube, int cubeIndex)
    {
        for (int i = 1; i < 7; i++)
        {
            stack1.Push(i);
            input1 = i;
            cubes[cubeIndex].side1Solver.number = i;
            cubes[cubeIndex].side1Solver.oposedSide.number = 7 - i;
            for (int j = 1; j < 7; j++)
            {
                bool input2Validity = checkIfInputValid(j, i);

                if (input2Validity)
                {
                    stack2.Push(j);
                    input2 = j;
                    cubes[cubeIndex].side2Solver.number = j;
                    cubes[cubeIndex].side2Solver.oposedSide.number = 7 - j;
                }
                for (int k = 1; k < 7; k++)
                {
                    bool input3Validity = checkIfInputValid(k, i, j);
                    if (input3Validity)
                    {
                        stack3.Push(k);
                        input3 = k;
                        cubes[cubeIndex].side3Solver.number = k;
                        cubes[cubeIndex].side3Solver.oposedSide.number = 7 - k;
                        Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3);
                        //Introducir dat
                        //comprobar si es correcto
                        cube1.UpdateCube();
                        cube2.UpdateCube();
                        if (cubes[cubeIndex].cubeCompleted)
                        {
                            Debug.Log("Posible Solucion, salvamos");
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                    return false;
                }
            }
        }
        return false;

    }

    public bool checkIfInputValid(int inputToTest, int compared1, int compared2)
    {


        if (compared1 == 0 || compared2 == 0) //used when initiating the stack
        {
            return false;
        }
        if (inputToTest < 1 || inputToTest > 6) //if not a correctNumber
        {
            return false;
        }
        if (inputToTest == compared1 || inputToTest == compared2 || compared2 == compared1) //not the same numbers
        {
            return false;
        }
        if (inputToTest + compared1 == 7 || inputToTest + compared2 == 7 || compared1 + compared2 == 7) //not the number of their oposed side
        {
            return false;
        }
        return true;
    }
    public bool checkIfInputValid(int inputToTest, int compared1)
    {
        if (compared1 == 0) //used when initiating the stack
        {
            return true;
        }
        if (inputToTest < 1 || inputToTest > 6) //if not a correctNumber
        {
            return false;
        }
        if (inputToTest == compared1) //not the same numbers
        {
            return false;
        }
        if (inputToTest + compared1 == 7) //not the number of their oposed side
        {
            return false;
        }
        return true;
    }
}


/// Recorrer (Cubeside side)
///     marcar nodo 1 como recorrido
///     marcar nodo 2 como recorrido
///     marcar nodo 3 como recorrido
///     
///     If solucion no es valida
///         sumar +=1 al nodo 3 hasta que nodo 3 != nodo1 o nodo 2, y nodo3 + nodo1 o 2 != 7
///         if nodo3 recorre todas las posibilidades y sigue sin ser valido
///             sumar +=1 al nodo 2 hasta que nodo 2 != nodo1 o nodo 3, y nodo2 + nodo1 o 3 != 7
///             if nodo2 recorre todas las posibilidades y no es valido
///                 sumar +=1 al nodo
///         
/// 
