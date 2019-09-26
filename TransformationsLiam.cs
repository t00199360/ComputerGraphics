using UnityEngine;
using System.Collections;
using System;

public class TransformationsLiam : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3[] cube = new Vector3[8];
        cube[0] = new Vector3(1, 1, 1);
        cube[1] = new Vector3(-1, 1, 1);
        cube[2] = new Vector3(-1, -1, 1);                   //define the initial cube
        cube[3] = new Vector3(1, -1, 1);
        cube[4] = new Vector3(1, 1, -1);
        cube[5] = new Vector3(-1, 1, -1);
        cube[6] = new Vector3(-1, -1, -1);
        cube[7] = new Vector3(1, -1, -1);

        Vector3 startingAxis = new Vector3(14, 2, 2);           //The axis it initially starts on
        startingAxis.Normalize();
        Quaternion rotation = Quaternion.AngleAxis(10, startingAxis);
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(new Vector3(0,0,0),                   //TRS = Translation, Rotation, Scale. Translation is not accounted for
                            rotation,                           // Rotation is accounted for.
                            Vector3.one);                       //Scale is not accounted for.

        


        printMatrix(rotationMatrix);

        Vector3[] ImageAfterRotation =
            MatrixTransform(cube, rotationMatrix);
        printMatrix(rotationMatrix);
        printVerts(ImageAfterRotation);

        Matrix4x4 scaleMatrix = Matrix4x4.TRS(new Vector3(0, 0, 0),                   //TRS = Translation, Rotation, Scale. Translation is not accounted for
                            Quaternion.identity,                          
                            new Vector3(14, 5, 2));

        printMatrix(scaleMatrix);

        Vector3[] ImageAfterScaling = MatrixTransform(ImageAfterRotation, scaleMatrix);

        printVerts(ImageAfterScaling);

        Matrix4x4 translateMatrix = Matrix4x4.TRS(new Vector3(-5, 1, -1),                   //TRS = Translation, Rotation, Scale. Translation is not accounted for
                     Quaternion.identity,                           
                     new Vector3(1,1,1));
        Vector3[] ImageAfterTranslation = MatrixTransform(ImageAfterScaling, translateMatrix);

        printMatrix(translateMatrix);

        printVerts(ImageAfterTranslation);

        Matrix4x4 single_matrix_of_transformations = translateMatrix * scaleMatrix *  rotationMatrix;

        Vector3[] ImageAfterTransformations = MatrixTransform(cube, single_matrix_of_transformations);
        printVerts(ImageAfterTransformations);

        
        

        Vector3 cameraPosition = new Vector3(16,5,52);
        Vector3 cameraLookAt = new Vector3(2,14,2);
        Vector3 cameraUp = new Vector3(-1,2,14);

        Vector3 lookRotDir = cameraLookAt - cameraPosition;
        Quaternion cameraRot = Quaternion.LookRotation(lookRotDir.normalized, cameraUp.normalized);

        Matrix4x4 viewMatrix = Matrix4x4.TRS(-cameraPosition, cameraRot,Vector3.one);       

        printMatrix(viewMatrix);               

        Vector3[] ImageAfterViewing = MatrixTransform(ImageAfterTransformations, viewMatrix);

        printVerts(ImageAfterViewing);

        //Direction = (lookAt - position)Normalised
        //Quaternion.LookAt()


        //Vector3 cameraDirection = new Vector3((cameraLookAt-cameraPosition).normalized);
        Quaternion.LookRotation((cameraLookAt-cameraPosition).normalized);

        

        Matrix4x4 perspMatrix = Matrix4x4.Perspective(90, 1.4f, 1, 1000);

        printMatrix(perspMatrix);

        Vector3[] Finalmage = MatrixTransform(ImageAfterViewing,perspMatrix);

        Matrix4x4 superMatrix = perspMatrix * viewMatrix * single_matrix_of_transformations;

        printMatrix(superMatrix);

        printVerts(Finalmage);


        Vector3[] FinalImageCheck = MatrixTransform(cube, superMatrix);

        printVerts(FinalImageCheck);
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
    void Update ()
    {
	
	}
}
