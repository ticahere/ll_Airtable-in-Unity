# ll_Airtable-in-Unity

This project demonstrates how to link an AirTable to the Unity scene. 
The `Virtual Art Gallery` scene reads from the Airtable base [`Artists_small`](https://airtable.com/appV5LgA8wXE1FnXZ/tbl6BD4rsBZ4ma5nw/viwbGD5PDBeyGNfSO?blocks=hide) and shows all images with the author names 
on play.

![alt text](/Screenshot/Capture.PNG)

Updating the `Artist_small` table, such as deleting one row, will change the art gallery scenes when the user hits play next time.
![alt text](/Screenshot/CaptureAfter.PNG)

## 1. Configure to connect to Airtable
* Inspect AirTableDB object in the `Virtual Art Gallery` scene
* Insert `App Key` and `Api Key` of your own Airtable account
    * You can find your Airtable Api Key under `Account Overview`
    * `App Key` is the string after "airtable.com/" url when you open your base. It should be started with 'app'
* Fill in the Table Name, `Artists_small` if you use the example base, or change to your Airtable base' sheet name, under `List Airtable Records` and `Get Airtable Record` script

## 2. Current data sheet structure
Currently Unity program reads  six columns from the `Artists_small`, defined in `BaseField.cs` file.
1. `Genre`: multiple strings describing the genre of the art
2. `Name`: name of the art or author
3. `Subtitle`: subtitle of the art
4. `Bio`: bio of the art or author
5. `url`: open link to the image
6. `ID`: order of the art


## 3. Change to your own data sheet
If you want to connect to your own Airtable base, follow the steps in *1. Configure to connect to Airtable* to change to your
`App Key`, `Api Key`, and data sheet name.

Make sure you follow the data field names in *2. Current data sheet structure* unless you want to customize them.


## 4. Customize Art Object and data structure
* If your data sheet has different column names, go to `BaseField.cs` to update to your column names and types.

* You can also modify `ArtObj` prefab or create your own art object you want the image from Airtable to be attached to. You only need to change the tag to "Art".

If you do either step above, you will need to make change in the DownloadImage function in the `GetAirtableRecord.cs` script.

## Step by step guide to create your own Virtual Gallery with Airtable without writing any code
1. Download this repository and unzip the folder.
2. Open the project in Unity version 2021.5 or newer.
3. Create an empty scene.
4. Create a floor by creating a Plane object.
5. Add `Assets/Prefab/AirtableDB.prefab` to the scene.
6. Follow *1. Configure to connect to Airtable* to configure your Airtable.
7. Hit play to test out. You should see the arts created in front of you.
8. Adjust direct light's rotation to make the arts visible (recommended rotation: `x: 25,  y:0, z:0`).
9. Add `Assets/FirstPerson AIO Pack/FirstPersonAIO/Prefab/FirstPerson-AIO.prefab` to the scene.
10. Hit play. Now you can walk around using arrow keys and see your arts!

**Create your own art gallery**
1. Create an empty game object and name it "Gallery".
2. Add multiple `Assets/Prefab/ArtObj.prefab` objects to the scene under Galery game object. The number of ArtObj should be the same as items in your Airtable.
3. Place each ArtObj at your desired place in the scene. You can add cube or plane as a wall to hang the ArtObj, or add lights to different place.
4. Uncheck `Auto Gallery` for AirtableDB under `Get Airtable Record`.
5. Hit play. The art images and texts from Airtable should be automatically attached to the gallery you create!
 
 Here is the example of `ArtGallery` scene.
 ![alt text](/Screenshot/Gallery.PNG)

## Credits
The project is built upon lipemon1's Airtable Unity Plugin examples [here](https://github.com/lipemon1/airtableunity). Huge thanks for their contributions to the open-sourced community.
