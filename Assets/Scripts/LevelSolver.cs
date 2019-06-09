using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSolver : MonoBehaviour
{
    public LevelEditor levelEditor;
    public List<string> solutions = new List<string>();

    public Queue<int> cube1Stack1 = new Queue<int>();
    public Queue<int> cube1Stack2 = new Queue<int>();
    public Queue<int> cube1Stack3 = new Queue<int>();

    public Queue<int> cube2Stack1 = new Queue<int>();
    public Queue<int> cube2Stack2 = new Queue<int>();
    public Queue<int> cube2Stack3 = new Queue<int>();

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

    public int index = 1;

    public void StartSolution()
    {
        start = false;
        input1 = 0;
        input2 = 0;
        input3 = 0;
    }
    public void Update()
    {
        if (levelEditor.cubes.Count > 1)
        {
            cubes = levelEditor.cubes;
        }
        if (start)
        {
            StartSolution();
        }
    }
    public void FillPossibleSolutionsRaw()
    {
        Debug.Log("Entering Coroutine");
       
            for (int i = 1; i < 7; i++)
            {
                cube1Stack1.Enqueue(i);
                //cube2Stack1.Enqueue(i);
                input1 = i;

                for (int j = 1; j < 7; j++)
                {
                    bool input2Validity = checkIfInputValid(j, i);

                    if (input2Validity)
                    {
                        cube1Stack2.Enqueue(j);
                        //cube2Stack2.Enqueue(j);
                        input2 = j;

                    }
                    for (int k = 1; k < 7; k++)
                    {
                        bool input3Validity = checkIfInputValid(k, i, j);

                        if (input3Validity)
                        {
                            cube1Stack3.Enqueue(k);
                            //cube2Stack3.Enqueue(k);
                            input3 = k;

                            Debug.Log("Valid Combination1: " + input1 + "," + input2 + "," + input3);

                        for (int a = 1; a < 7; a++)
                        {
                            cube2Stack1.Enqueue(a);
                            input1 = a;

                            for (int b = 0; b < 7; b++)
                            {
                                input2Validity = checkIfInputValid(b, a);

                                if (input2Validity)
                                {
                                    cube2Stack2.Enqueue(j);
                                    input2 = j;

                                }
                            }
                        }

                    }
                    }
                }
            }
        

    }



    public void CheckSolution()
    {


        for (int x = 0; x < cubes.Count; x++)
        {
            if (x == 0)
            {
                for (int i = 1; i < cube1Stack1.Count; i++)
                {
                    int cube1Side1 = cube1Stack1.Dequeue();

                    for (int j = 1; j < cube1Stack2.Count; j++)
                    {
                        int cube1Side2 = cube1Stack2.Dequeue();

                        for (int k = 1; k < cube1Stack3.Count; k++)
                        {
                            int cube1Side3 = cube1Stack3.Dequeue();


                            cubes[x].side1Solver.number = cube1Side1;
                            cubes[x].side2Solver.number = cube1Side2;
                            cubes[x].side3Solver.number = cube1Side3;


                            cubes[x].side1Solver.oposedSide.number = 7 - cube1Side1;
                            cubes[x].side2Solver.oposedSide.number = 7 - cube1Side2;
                            cubes[x].side3Solver.oposedSide.number = 7 - cube1Side3;

                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i < cube2Stack1.Count; i++)
                {
                    int cube2Side1 = cube2Stack1.Dequeue();

                    for (int j = 1; j < cube2Stack2.Count; j++)
                    {
                        int cube2Side2 = cube2Stack2.Dequeue();

                        for (int k = 1; k < cube2Stack3.Count; k++)
                        {
                            int cube2Side3 = cube2Stack3.Dequeue();

                            cubes[x].side1Solver.number = cube2Side1;
                            cubes[x].side2Solver.number = cube2Side2;
                            cubes[x].side3Solver.number = cube2Side3;


                            cubes[x].side1Solver.oposedSide.number = 7 - cube2Side1;
                            cubes[x].side2Solver.oposedSide.number = 7 - cube2Side2;
                            cubes[x].side3Solver.oposedSide.number = 7 - cube2Side3;

                            cubes[0].UpdateCube();
                            cubes[1].UpdateCube();


                            


                        }
                    }
                }
            }
            
        }

    }

    public void StartTestSolution()
    {
        int cube1Side1 = cube1Stack1.Dequeue();
        int cube1Side2 = cube1Stack2.Dequeue();
        int cube1Side3 = cube1Stack3.Dequeue();

        cubes[0].side1Solver.number = cube1Side1;
        cubes[0].side2Solver.number = cube1Side2;
        cubes[0].side3Solver.number = cube1Side3;

        cubes[0].side1Solver.oposedSide.number = 7 - cube1Side1;
        cubes[0].side2Solver.oposedSide.number = 7 - cube1Side2;
        cubes[0].side3Solver.oposedSide.number = 7 - cube1Side3;

        int cube2Side1 = cube2Stack1.Dequeue();
        int cube2Side2 = cube2Stack2.Dequeue();
        int cube2Side3 = cube2Stack3.Dequeue();

        cubes[1].side1Solver.number = cube2Side1;
        cubes[1].side2Solver.number = cube2Side2;
        cubes[1].side3Solver.number = cube2Side3;

        cubes[1].side1Solver.oposedSide.number = 7 - cube2Side1;
        cubes[1].side2Solver.oposedSide.number = 7 - cube2Side2;
        cubes[1].side3Solver.oposedSide.number = 7 - cube2Side3;

        bool levelValid = checkNumbersSolution();

    }


    public bool checkNumbersSolution()
    {
        int cube2Side3 = cube2Stack3.Dequeue();
        cubes[1].side3Solver.number = cube2Side3;
        cubes[1].side3Solver.oposedSide.number = 7 - cube2Side3;

        cubes[0].UpdateCube();
        cubes[1].UpdateCube();

        if (!cubes[0].CheckCompletion() || !cubes[1].CheckCompletion())
        {
            index++;
            return checkNumbersSolution();
        }
        return true;
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
