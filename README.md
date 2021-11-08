# Codra Panorama E2 KNX

Cet objet utilisateur permet l'intégration du bus KNX avec le logiciel Codra Panorama E2 via une passerelle IP/KNX.

Une application de référence est fournie avec tous les descripteurs de classes nécessaires.

Un objet de paramétrage permet de générer les sous-objets correspondant aux adresses de groupe d'une installation KNX à partir du fichier d'export XML d'ETS.

Ne prend pas en charge tous les datas points types, ils seront intégrés dans des développements futur.

**Utilisation**

- Ajouter l'application REF-ST-KNX dans les applications de référence de votre application.
- Instancier un nouveau composant KNX_Panorama dans votre application.
- Ouvrir les propriétés de ce composant pour importer le fichier XML contenant les adresses de groupe généré avec ETS.
- Renseigner l'adresse IP de la passerelle KNX.
- Mettre en place les liens entre les propriétés "Value_Sync" des objets de commande et les propriétés "Value" des objets de retour d'état.
- Connecter la propriété Is_Connected avec la propriété Read_Value des sous-objet afin de synchroniser les états des sous-objets au démarrage de l'application.

**Propriétés sous objet**

Chaque sous-objet correspond à une adresse de groupe est contient les propriétés suivantes :

- Group_Address   -> Adresse de groupe KNX (ex 0/0/1)
- Object_Name     -> Nom de l'objet
- Key             -> Référence unique de l'objet (utilisation interne)
- DPT             -> Data point type de l'adresse de groupe
- DPT_Name        -> Data point type format texte

- Value           -> Valeur de l'adresse de groupe
- Value_Sync      -> Valeur de synchronisation à lier avec un retour d'état
- Read_Value      -> Demande de lecture de la valeur de l'adresse de groupe

v1.0
Initial release
