using UnityEngine;
using System.Collections;
using System;

public class Transformations : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3[] cube = new Vector3[8];
        cube[0] = new Vector3(1, 1, 1);
        cube[1] = new Vector3(-1, 1, 1);
        cube[2] = new Vector3(-1, -1, 1);
        cube[3] = new Vector3(1, -1, 1);
        cube[4] = new Vector3(1, 1, -1);
        cube[5] = new Vector3(-1, 1, -1);
        cube[6] = new Vector3(-1, -1, -1);
        cube[7] = new Vector3(1, -1, -1);

        Vector3 startingAxis = new Vector3(14, 2, 2);
        startingAxis.Normalize();
        Quaternion rotation = Quaternion.AngleAxis(10, startingAxis);
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(new Vector3(0,0,0),
                            rotation,
                            Vector3.one);
        printMatrix(rotationMatrix);

        Vector3[] ImageAfterRotation =
            MatrixTransform(cube, rotationMatrix);
        printVerts(ImageAfterRotation);
        
	}

    private void matrixScale()
    {
        Vector3[] cube = new Vector3[8];
        cube[0] = new Vector3(1, 1, 1);
        cube[1] = new Vector3(-1, 1, 1);            //change the cube to the previous results
        cube[2] = new Vector3(-1, -1, 1);
        cube[3] = new Vector3(1, -1, 1);
        cube[4] = new Vector3(1, 1, -1);
        cube[5] = new Vector3(-1, 1, -1);
        cube[6] = new Vector3(-1, -1, -1);
        cube[7] = new Vector3(1, -1, -1);

        Vector3 startingAxis = new Vector3(14, 2, 2);
        startingAxis.Normalize();                                       //change names from  rotation to scale matrix
        Quaternion rotation = Quaternion.AngleAxis(10, startingAxis);
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(new Vector3(0, 0, 0),
                            Quaternion.identity, new Vector3(0,0,0));
        printMatrix(rotationMatrix);

        Vector3[] ImageAfterRotation =
            MatrixTransform(cube, rotationMatrix);
        printVerts(ImageAfterRotation);
    }

    private void printVerts(Vector3[] newImage)
    {
        for (int i = 0; i < newImage.Length; i++)
            print(newImage[i].x + " , " +
                newImage[i].y + " , " +
                newImage[i].z);
    }

    private Vector3[] MatrixTransform(
        Vector3[] meshVertices, 
        Matrix4x4 transformMatrix)
    {
        Vector3[] output = new Vector3[meshVertices.Length];
        for (int i = 0; i < meshVertices.Length; i++)
            output[i] = transformMatrix * 
                new Vector4( 
                meshVertices[i].x,
                meshVertices[i].y,
                meshVertices[i].z,
                    1);

        return output;
    }

    private void printMatrix(Matrix4x4 matrix)
    {
        for (int i = 0; i < 4; i++)
            print(matrix.GetRow(i).ToString());
    }



    // Update is called once per frame
    void Update () {
	
	}
}
