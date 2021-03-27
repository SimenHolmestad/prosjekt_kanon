# Prosjekt kanon

Dette er prosjektet til gruppe 6 i landsbyen "VR og AR innen læring og trening" for våren 2021.

# Hvordan kjøre prosjektet på oculus quest

Etter du har lastet ned prosjektet er du nødt til å installere pluginen "Oculus integration" i Unity Asset Store. Det ble bare kødd da vi prøvde å ha denne liggende inne i git.

Vi brukte [denne guiden](https://myvrprofessor.com/how-to-install-your-unity-game-on-oculus-quest-or-oculus-go/?fbclid=IwAR3sHoxXXsOx24wAWc9RZQuEwy_GSl3TghpWgblCFvumiSgZMAHlCVO3Vpk) for å sette opp alt riktig.

Spesielt viktig er det å sørge for at Unity-versjonen du bruker (som du finner i UnityHub) er bygget med såkalt "android build support". Hvis den er dette vil du få en liten android-figur på den. Den kan hende at du er nødt til å laste ned Unity-versjonen helt på nytt for å få Android build support :(

Etter man har fulgt alle stegene i guiden kan man koble Oculus-brillene til maskinen med USB og trykke seg inn på "File -> Build Settings", for deretter å trykke på "Build And Run" nederst til høyre i menyen som dukker opp.

# Git lfs (large file storage)

For å få lastet ned alle filene må man sørge for at git lfs er installert på maskinen og kjøre:

```
git lfs fetch
```

