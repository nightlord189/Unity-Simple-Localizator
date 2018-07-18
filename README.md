# Unity Simple Localizator
Simple localization plugin for Unity3D game engine.

[Download link](https://github.com/alexdebur/Unity-Simple-Localizator/releases)

## Quick start
#### 1. [Download](https://github.com/alexdebur/Unity-Simple-Localizator/releases) and import package to your project (Assets->Import Package->Custom Package).
&nbsp; &nbsp;&nbsp; &nbsp;

#### 2. For example, let's do the Image components multilangual
Select your Image gameobject and add MultiLangImage component to it:
![ex](https://i.paste.pics/3EFOG.png)

Assign your images of each language what you want to translate to the Translations array:
![ex](https://i.paste.pics/3EFR7.png)

Now you can see how component translates your Image by changing the Current Language property of this component:
![ex](https://i.paste.pics/3EFQ6.png)
&nbsp; &nbsp;&nbsp; &nbsp;



#### 3. Multilang texts, meshes, materials, sprites
Similarly, you can do these actions with other Unity components of content: Sprites, Meshes, Materials, UI text, GameObject. Just select the component (MultiLangSimpleText, MultiLangMaterial, MultiLangAudio, MultiLangVideo and etc), add it to GameObject and setup Translations array!
![ex](https://i.paste.pics/3EFSU.png)
&nbsp; &nbsp;&nbsp; &nbsp;



#### 4. Changing application language
To change the app language, add the LanguageManager component to any gameobject on your scene:
![ex](https://i.paste.pics/3EFY6.png)
If Auto Detect Language property is checking, LanguageManager automatically defines language by user's system on start. You always can change current language in runtime.

For example, we are creating the Button and adding LanguageManager's event SetEnglishLang to OnPress:
![ex](https://i.paste.pics/3EG17.png)
Similarly, you can call SetEnglishLang, SetRussianLang, SetKazakhLang.
&nbsp; &nbsp;&emsp;


If you need to add more languages, just add it to Language enum in SimpleLocalizator/Data/Language.cs.


### Author site: [NLDevelop.com](https://nldevelop.com).
