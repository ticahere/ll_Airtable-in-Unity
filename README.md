# LL_Airtable-in-Unity

This project demonstrates how to link an AirTable to the Unity scene. 
The `ArtGallery` scene reads from the table [`Artists_small`](https://airtable.com/appV5LgA8wXE1FnXZ/tbl6BD4rsBZ4ma5nw/viwbGD5PDBeyGNfSO?blocks=hide) and shows all images with the author names 
on play.

![alt text](/Screenshot/Capture.PNG)

Updating the `Artist_small` table, such as deleting one row, will change the art gallery scenes when the user hits play next time.
![alt text](/Screenshot/CaptureAfter.PNG)

## 1. Configure to connect to Airtable
* Inspect AirTableExamples object in the ArtGallery scene
* Insert `App Key` and `Api Key`
* Fill in the Table Name, `Artists_small`, under `List Record` and `Get Record Examples` script

## 2. Current data sheet structure
Currently Unity program reads  four columns from the `Artists_small`, defined in `BaseField.cs` file.
1. `Genre`: multiple strings describing the genre of the art
2. `Name`: name of the art or author
2. `Bio`: bio of the art or author
3. `url`: open link to the image


## 3. Change to your own data sheet
If you want to connect to your own Airtable base, follow the steps in *1. Configure to connect to Airtable* to change to your
`App Key`, `Api Key`, and data sheet name.

Make sure you follow the data field names in *2. Current data sheet structure* unless you want to customize them.

## 4. Customize Art Object and data structure
* If your data sheet has different column names, go to `BaseField.cs` to update to your column names and types.

* You can also create different Game Object Prefab to replace `Art Object` under `Get Record Examples` script of AirTableExamples.

If you do either step above, you will need to make change in the DownloadImage function in the `GetRecordExamples.cs` script.
