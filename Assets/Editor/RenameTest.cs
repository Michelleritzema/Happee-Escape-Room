using UnityEngine;
using NUnit.Framework;

/*
 * Created by Michelle Ritzema.
 * 
 * Test to create and rename a new GameObject.
 */
public class RenameTest {

    [Test]
    public void EditorTest()
    {
        var gameObject = new GameObject();

        //Act
        var newGameObjectName = "My game object";
        gameObject.name = newGameObjectName;

        //Assert
        Assert.AreEqual(newGameObjectName, gameObject.name);
    }

}