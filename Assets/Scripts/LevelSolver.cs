﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSolver : MonoBehaviour
{
    public LevelEditor levelEditor;
    public RandomLevelCreator randomLevelCreator;
    public List<string> solutions = new List<string>();
    public List<int> solutionInts = new List<int>();

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
    public bool startCoroutine;

    public int input1;
    public int input2;
    public int input3;
    public int input4;
    public int input5;
    public int input6;
    public int input7;
    public int input8;
    public int input9;

    int cube1Side1;
    int cube1Side2;
    int cube1Side3;
    int cube2Side1;
    int cube2Side2;
    int cube2Side3;

    public int index = 1;
    public int sidesSamePos = 1;
    public bool solvedLevel;

    public void StartSolution()
    {
        solutionInts.Clear();
        input1 = 0;
        input2 = 0;
        input3 = 0;
        input4 = 0;
        input5 = 0;
        input6 = 0;
        input7 = 0;
        input8 = 0;
        input9 = 0;

        for (int i = 0; i < cubes.Count * 3; i++) //init the list so we can properly insert
        {
            solutionInts.Add(0);
        }
        index = 1;
        if (start) CreateSolution();
        else if (startCoroutine) StartCoroutine(CreateSolutionCoroutine());
        start = false;
        startCoroutine = false;
    }
    public void Update()
    {
        //cubes = randomLevelCreator.cubesCreated;
        cubes = levelEditor.cubes;
        if (cubes.Count < 1)
        {
            return;
        }
        if (start || startCoroutine)
        {
            StartSolution();
        }
    }
    public void CreateSolution()
    {
        Debug.Log("Entering Coroutine");
        index = 1;

        levelEditor.CheckForDuplicates();


        for (int a = 1; a < 7; a++)
        {
            solutionInts[0] = a;
            input1 = a;

            for (int b = 1; b < 7; b++)
            {
                bool input2Validity = checkIfInputValid(b, a);

                if (input2Validity)
                {
                    solutionInts[1] = b;
                    input2 = b;

                    for (int c = 1; c < 7; c++)
                    {
                        bool input3Validity = checkIfInputValid(c, a, b);

                        if (input3Validity)
                        {
                            solutionInts[2] = c;
                            input3 = c;

                            for (int d = 1; d < 7; d++)
                            {
                                solutionInts[3] = d;
                                input4 = d;

                                for (int e = 1; e < 7; e++)
                                {
                                    input2Validity = checkIfInputValid(e, d);

                                    if (input2Validity)
                                    {
                                        solutionInts[4] = e;
                                        input5 = e;

                                        for (int f = 1; f < 7; f++)
                                        {
                                            input3Validity = checkIfInputValid(f, d, e);

                                            if (input3Validity)
                                            {
                                                solutionInts[5] = f;
                                                input6 = f;

                                                if (cubes.Count == 2)
                                                {
                                                    Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3 + " 2: " + input4 + "," + input5 + "," + input6);
                                                    bool testSolution = InputTestSolution(solutionInts);
                                                    if (testSolution) return;


                                                }
                                                else
                                                {
                                                    for (int g = 1; g < 7; g++)
                                                    {
                                                        input7 = g;
                                                        solutionInts[6] = g;

                                                        for (int h = 1; h < 7; h++)
                                                        {
                                                            input2Validity = checkIfInputValid(h, g);

                                                            if (input2Validity)
                                                            {
                                                                solutionInts[7] = h;
                                                                input8 = h;

                                                                for (int i = 1; i < 7; i++)
                                                                {
                                                                    input3Validity = checkIfInputValid(i, g, h);

                                                                    if (input3Validity)
                                                                    {
                                                                        input9 = i;
                                                                        solutionInts[8] = i;
                                                                        if (cubes.Count == 3)
                                                                        {
                                                                            //Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3 + " 2: " + input4 + "," + input5 + "," + input6);
                                                                            bool testSolution = InputTestSolution(solutionInts);
                                                                            if (testSolution) return;


                                                                        }
                                                                        else
                                                                        {
                                                                            for (int j = 1; j < 7; j++)
                                                                            {
                                                                                input7 = j;
                                                                                solutionInts[9] = j;

                                                                                for (int k = 1; k < 7; k++)
                                                                                {
                                                                                    input2Validity = checkIfInputValid(k, j);

                                                                                    if (input2Validity)
                                                                                    {
                                                                                        solutionInts[10] = k;
                                                                                        input8 = k;

                                                                                        for (int m = 1; m < 7; m++)
                                                                                        {
                                                                                            input3Validity = checkIfInputValid(m, j, k);

                                                                                            if (input3Validity)
                                                                                            {
                                                                                                input9 = m;
                                                                                                solutionInts[11] = m;
                                                                                                if (cubes.Count == 4)
                                                                                                {
                                                                                                    //Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3 + " 2: " + input4 + "," + input5 + "," + input6);
                                                                                                    bool testSolution = InputTestSolution(solutionInts);
                                                                                                    if (testSolution) return;

                                                                                                }
                                                                                                else
                                                                                                {


                                                                                                    for (int n = 1; n < 7; n++)
                                                                                                    {
                                                                                                        input7 = n;
                                                                                                        solutionInts[12] = n;

                                                                                                        for (int o = 1; o < 7; o++)
                                                                                                        {
                                                                                                            input2Validity = checkIfInputValid(o, n);

                                                                                                            if (input2Validity)
                                                                                                            {
                                                                                                                solutionInts[13] = o;
                                                                                                                input8 = o;

                                                                                                                for (int p = 1; p < 7; p++)
                                                                                                                {
                                                                                                                    input3Validity = checkIfInputValid(p, n, o);

                                                                                                                    if (input3Validity)
                                                                                                                    {
                                                                                                                        input9 = p;
                                                                                                                        solutionInts[14] = p;
                                                                                                                        if (cubes.Count == 5)
                                                                                                                        {
                                                                                                                            //Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3 + " 2: " + input4 + "," + input5 + "," + input6);
                                                                                                                            solvedLevel = InputTestSolution(solutionInts);
                                                                                                                            bool testSolution = InputTestSolution(solutionInts);
                                                                                                                            if (testSolution) return;

                                                                                                                        }
                                                                                                                        else
                                                                                                                        {


                                                                                                                            for (int q = 1; q < 7; q++)
                                                                                                                            {
                                                                                                                                input7 = q;
                                                                                                                                solutionInts[15] = q;

                                                                                                                                for (int r = 1; r < 7; r++)
                                                                                                                                {
                                                                                                                                    input2Validity = checkIfInputValid(r, q);

                                                                                                                                    if (input2Validity)
                                                                                                                                    {
                                                                                                                                        solutionInts[16] = r;
                                                                                                                                        input8 = r;

                                                                                                                                        for (int s = 1; s < 7; s++)
                                                                                                                                        {
                                                                                                                                            input3Validity = checkIfInputValid(s, q, r);

                                                                                                                                            if (input3Validity)
                                                                                                                                            {
                                                                                                                                                input9 = s;
                                                                                                                                                solutionInts[17] = s;
                                                                                                                                                if (cubes.Count == 6)
                                                                                                                                                {
                                                                                                                                                    //Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3 + " 2: " + input4 + "," + input5 + "," + input6);
                                                                                                                                                    solvedLevel = InputTestSolution(solutionInts);
                                                                                                                                                    bool testSolution = InputTestSolution(solutionInts);
                                                                                                                                                    if (testSolution) return;

                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {


                                                                                                                                                for (int t = 1; t < 7; t++)
                                                                                                                                                {
                                                                                                                                                    input7 = t;
                                                                                                                                                    solutionInts[18] = t;

                                                                                                                                                    for (int u = 1; u < 7; u++)
                                                                                                                                                    {
                                                                                                                                                        input2Validity = checkIfInputValid(u, t);

                                                                                                                                                        if (input2Validity)
                                                                                                                                                        {
                                                                                                                                                            solutionInts[19] = u;
                                                                                                                                                            input8 = u;

                                                                                                                                                            for (int v = 1; v < 7; v++)
                                                                                                                                                            {
                                                                                                                                                                input3Validity = checkIfInputValid(v, t, u);

                                                                                                                                                                if (input3Validity)
                                                                                                                                                                {
                                                                                                                                                                    input9 = v;
                                                                                                                                                                    solutionInts[20] = v;
                                                                                                                                                                    if (cubes.Count == 7)
                                                                                                                                                                    {
                                                                                                                                                                        //Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3 + " 2: " + input4 + "," + input5 + "," + input6);
                                                                                                                                                                        solvedLevel = InputTestSolution(solutionInts);
                                                                                                                                                                        bool testSolution = InputTestSolution(solutionInts);
                                                                                                                                                                        if (testSolution) return;

                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }

                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }

                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }

                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
        if (!validCombination)
        {
            randomLevelCreator.LevelGenerator(solutionInts.Count / 3, sidesSamePos);
        }


        Debug.Log("Este nivel no se puede resolver");
    }
    public IEnumerator CreateSolutionCoroutine()
    {
        levelEditor.CheckForDuplicates();
        Debug.Log("Entering Coroutine");

        //index = 1;
        if (index >= 2300)
        {
            startCoroutine = false;
            yield break;
        }


        for (int a = 1; a < 7; a++)
        {
            solutionInts[0] = a;
            input1 = a;

            for (int b = 1; b < 7; b++)
            {
                bool input2Validity = checkIfInputValid(b, a);

                if (input2Validity)
                {
                    solutionInts[1] = b;
                    input2 = b;

                    for (int c = 1; c < 7; c++)
                    {
                        bool input3Validity = checkIfInputValid(c, a, b);

                        if (input3Validity)
                        {
                            solutionInts[2] = c;
                            input3 = c;

                            for (int d = 1; d < 7; d++)
                            {
                                solutionInts[3] = d;
                                input4 = d;

                                for (int e = 1; e < 7; e++)
                                {
                                    input2Validity = checkIfInputValid(e, d);

                                    if (input2Validity)
                                    {
                                        solutionInts[4] = e;
                                        input5 = e;

                                        for (int f = 1; f < 7; f++)
                                        {
                                            input3Validity = checkIfInputValid(f, d, e);

                                            if (input3Validity)
                                            {
                                                solutionInts[5] = f;
                                                input6 = f;

                                                if (cubes.Count == 2)
                                                {
                                                    Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3 + " 2: " + input4 + "," + input5 + "," + input6);
                                                    solvedLevel = InputTestSolution(solutionInts);
                                                    if (solvedLevel) yield break;
                                                    yield return null;

                                                }
                                                else
                                                {
                                                    for (int g = 1; g < 7; g++)
                                                    {
                                                        input7 = g;
                                                        solutionInts[6] = g;

                                                        for (int h = 1; h < 7; h++)
                                                        {
                                                            input2Validity = checkIfInputValid(h, g);

                                                            if (input2Validity)
                                                            {
                                                                solutionInts[7] = h;
                                                                input8 = h;

                                                                for (int i = 1; i < 7; i++)
                                                                {
                                                                    input3Validity = checkIfInputValid(i, g, h);

                                                                    if (input3Validity)
                                                                    {
                                                                        input9 = i;
                                                                        solutionInts[8] = i;
                                                                        if (cubes.Count == 3)
                                                                        {
                                                                            //Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3 + " 2: " + input4 + "," + input5 + "," + input6);
                                                                            solvedLevel = InputTestSolution(solutionInts);
                                                                            if (solvedLevel) yield break;
                                                                            yield return null;

                                                                        }
                                                                        else
                                                                        {
                                                                            for (int j = 1; j < 7; j++)
                                                                            {
                                                                                input7 = j;
                                                                                solutionInts[9] = j;

                                                                                for (int k = 1; k < 7; k++)
                                                                                {
                                                                                    input2Validity = checkIfInputValid(k, j);

                                                                                    if (input2Validity)
                                                                                    {
                                                                                        solutionInts[10] = k;
                                                                                        input8 = k;

                                                                                        for (int m = 1; m < 7; m++)
                                                                                        {
                                                                                            input3Validity = checkIfInputValid(m, j, k);

                                                                                            if (input3Validity)
                                                                                            {
                                                                                                input9 = m;
                                                                                                solutionInts[11] = m;
                                                                                                if (cubes.Count == 4)
                                                                                                {
                                                                                                    //Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3 + " 2: " + input4 + "," + input5 + "," + input6);
                                                                                                    solvedLevel = InputTestSolution(solutionInts);
                                                                                                    if (solvedLevel) yield break;
                                                                                                    yield return null;

                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    for (int n = 1; n < 7; n++)
                                                                                                    {
                                                                                                        input7 = n;
                                                                                                        solutionInts[12] = n;

                                                                                                        for (int o = 1; o < 7; o++)
                                                                                                        {
                                                                                                            input2Validity = checkIfInputValid(o, n);

                                                                                                            if (input2Validity)
                                                                                                            {
                                                                                                                solutionInts[13] = o;
                                                                                                                input8 = o;

                                                                                                                for (int p = 1; p < 7; p++)
                                                                                                                {
                                                                                                                    input3Validity = checkIfInputValid(p, n, o);

                                                                                                                    if (input3Validity)
                                                                                                                    {
                                                                                                                        input9 = p;
                                                                                                                        solutionInts[14] = p;
                                                                                                                        if (cubes.Count == 5)
                                                                                                                        {
                                                                                                                            //Debug.Log("Valid Combination: " + input1 + "," + input2 + "," + input3 + " 2: " + input4 + "," + input5 + "," + input6);
                                                                                                                            solvedLevel = InputTestSolution(solutionInts);
                                                                                                                            if (solvedLevel) yield break;
                                                                                                                            yield return null;

                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }

        startCoroutine = false;

        Debug.Log("Este nivel no se puede resolver");
    }

    public void CreateSolCoroutine(int iterations, int Ncubes, bool firstCall)
    {
        Debug.Log("Entering Coroutine");
        //All before will be called one time
        firstCall = false;
        if (index >= 2300 || iterations >= Ncubes) //if we go overrails
        {
            startCoroutine = false;
            return;
        }
        Debug.Log(iterations.ToString() + Ncubes.ToString());
        for (int i = 0; i < 7; i++) // 2^(3n-2) wolfram series 2,16,128,1024...
        {
            solutionInts[0 + 3 * iterations] = i;

            for (int j = 0; j < 7; j++)
            {
                bool input2Validity = checkIfInputValid(j, i);

                if (input2Validity)
                {
                    solutionInts[1 + 3 * iterations] = j;

                    for (int k = 0; k < 7; k++)
                    {
                        bool input3Validity = checkIfInputValid(k, i, j);

                        if (input3Validity)
                        {
                            solutionInts[2 + 3 * iterations] = k;
                            if (iterations + 1 == Ncubes) //if this is the last iteration to input
                            {
                                bool testValidity = InputTestSolution(solutionInts);
                                if (testValidity)
                                {
                                    return;
                                }
                                else
                                {
                                    CreateSolCoroutine(0, Ncubes, false); //restart the solution
                                    return;
                                }
                            }
                            else
                            {
                                CreateSolCoroutine(iterations + 1, Ncubes, false);
                            }

                        }
                    }
                }
            }
        }
    }

    public bool InputTestSolution(int c1s1, int c1s2, int c1s3, int c2s1, int c2s2, int c2s3)
    {
        cubes[0].side1Solver.number = c1s1;
        cubes[0].side2Solver.number = c1s2;
        cubes[0].side3Solver.number = c1s3;

        cubes[0].side1Solver.oposedSide.number = 7 - c1s1;
        cubes[0].side2Solver.oposedSide.number = 7 - c1s2;
        cubes[0].side3Solver.oposedSide.number = 7 - c1s3;

        cubes[1].side1Solver.number = c2s1;
        cubes[1].side2Solver.number = c2s2;
        cubes[1].side3Solver.number = c2s3;

        cubes[1].side1Solver.oposedSide.number = 7 - c2s1;
        cubes[1].side2Solver.oposedSide.number = 7 - c2s2;
        cubes[1].side3Solver.oposedSide.number = 7 - c2s3;

        cubes[0].UpdateCube();
        cubes[1].UpdateCube();

        if (!cubes[0].CheckCompletion() || !cubes[1].CheckCompletion())
        {
            return false;
        }
        return true;

    }
    public bool InputTestSolution(int c1s1, int c1s2, int c1s3, int c2s1, int c2s2, int c2s3, int c3s1, int c3s2, int c3s3)
    {
        cubes[0].side1Solver.number = c1s1;
        cubes[0].side2Solver.number = c1s2;
        cubes[0].side3Solver.number = c1s3;

        cubes[0].side1Solver.oposedSide.number = 7 - c1s1;
        cubes[0].side2Solver.oposedSide.number = 7 - c1s2;
        cubes[0].side3Solver.oposedSide.number = 7 - c1s3;

        cubes[1].side1Solver.number = c2s1;
        cubes[1].side2Solver.number = c2s2;
        cubes[1].side3Solver.number = c2s3;

        cubes[1].side1Solver.oposedSide.number = 7 - c2s1;
        cubes[1].side2Solver.oposedSide.number = 7 - c2s2;
        cubes[1].side3Solver.oposedSide.number = 7 - c2s3;

        cubes[2].side1Solver.number = c3s1;
        cubes[2].side2Solver.number = c3s2;
        cubes[2].side3Solver.number = c3s3;

        cubes[2].side1Solver.oposedSide.number = 7 - c3s1;
        cubes[2].side2Solver.oposedSide.number = 7 - c3s2;
        cubes[2].side3Solver.oposedSide.number = 7 - c3s3;

        cubes[0].UpdateCube();
        cubes[1].UpdateCube();
        cubes[2].UpdateCube();

        if (!cubes[0].CheckCompletion() || !cubes[1].CheckCompletion() || !cubes[2].CheckCompletion())
        {
            return false;
        }
        validCombination = true;
        return true;
    }

    public bool InputTestSolution(List<int> inputsToTestSolution)
    {
        index++;
        for (int i = 0; i < cubes.Count; i++)
        {
            cubes[i].side1Solver.number = inputsToTestSolution[0 + 3 * i];
            cubes[i].side2Solver.number = inputsToTestSolution[1 + 3 * i];
            cubes[i].side3Solver.number = inputsToTestSolution[2 + 3 * i];

            cubes[i].side1Solver.oposedSide.number = 7 - inputsToTestSolution[0 + 3 * i];
            cubes[i].side2Solver.oposedSide.number = 7 - inputsToTestSolution[1 + 3 * i];
            cubes[i].side3Solver.oposedSide.number = 7 - inputsToTestSolution[2 + 3 * i];

        }

        foreach (Cube cube in cubes)
        {
            cube.UpdateCube();
        }

        foreach (Cube cube in cubes)
        {
            if (!cube.CheckCompletion())
            {
                return false;
            }
        }
        Debug.Log("LevelSolved");
        validCombination = true;
        return true;
    }

    public IEnumerator CheckNumbersSolution()
    {
        levelEditor.CheckForDuplicates();
        cubes[0].UpdateCube();
        cubes[1].UpdateCube();

        while (!cubes[0].CheckCompletion() || !cubes[1].CheckCompletion())
        {
            Debug.Log("Invalid Combination: " + cube1Side1 + "," + cube1Side2 + "," + cube1Side3 + " 2: " + cube2Side1 + "," + cube2Side2 + "," + cube2Side3);
            cube2Side3 = cube2Stack3.Dequeue();
            cubes[1].side3Solver.number = cube2Side3;
            cubes[1].side3Solver.oposedSide.number = 7 - cube2Side3;

            if (index % 2 == 0)
            {
                cube2Side2 = cube2Stack2.Dequeue();
                cubes[1].side2Solver.number = cube2Side2;
                cubes[1].side2Solver.oposedSide.number = 7 - cube2Side2;

                if (index % 8 == 0)
                {

                    cube2Side1 = cube2Stack1.Dequeue();
                    cubes[1].side1Solver.number = cube2Side1;
                    cubes[1].side1Solver.oposedSide.number = 7 - cube2Side1;

                    if (index % 48 == 0)
                    {
                        cube1Side3 = cube1Stack3.Dequeue();
                        cubes[0].side3Solver.number = cube1Side3;
                        cubes[0].side3Solver.oposedSide.number = 7 - cube1Side3;

                        if (index % 96 == 0)
                        {
                            cube1Side2 = cube1Stack2.Dequeue();
                            cubes[0].side2Solver.number = cube1Side2;
                            cubes[0].side2Solver.oposedSide.number = 7 - cube1Side2;

                            if (index % 384 == 0)
                            {
                                cube1Side1 = cube1Stack1.Dequeue();
                                cubes[0].side1Solver.number = cube1Side1;
                                cubes[0].side1Solver.oposedSide.number = 7 - cube1Side1;
                            }
                        }
                    }
                }
            }
            //Debug.Log(cube1Stack1.Count);
            index++;

            cubes[0].UpdateCube();
            cubes[1].UpdateCube();
            yield return new WaitForSeconds(0.01f);

        }
        yield return new WaitForEndOfFrame();
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

    public void StartButton()
    {
        solutionInts.Clear();
        start = true;
    }
    public void StartButtonCoroutine()
    {
        solutionInts.Clear();
        startCoroutine = true;
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
