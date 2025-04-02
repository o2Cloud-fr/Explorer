# 🗂️ Explorer - Gestionnaire de Fichiers Windows Personnalisé

**Explorer** est une application de gestion de fichiers légère et intuitive, développée en C# avec Windows Forms. Elle offre une interface familière pour naviguer dans votre système de fichiers avec la robustesse et la personnalisation d'une application native.

![Banner](https://o2cloud.fr/logo/o2Cloud.png)

## ✨ Fonctionnalités

- 🌲 **Navigation arborescente** - Interface à deux panneaux avec arborescence de dossiers
- 📁 **Visualisation détaillée** - Affichage des informations essentielles des fichiers
- 🖼️ **Icônes système** - Utilisation des icônes Windows natives pour une meilleure lisibilité
- 📏 **Taille de fichiers formatée** - Conversion automatique en KB, MB, GB ou TB
- 🔍 **Exploration intuitive** - Double-clic pour ouvrir les fichiers avec leur application par défaut
- 🛡️ **Gestion des erreurs** - Protection contre les erreurs d'accès aux fichiers
- 🌍 **Interface en français** - Colonnes et messages localisés

## 📋 Pré-requis

- Système d'exploitation Windows
- .NET Framework 4.8 ou supérieur

## 🚀 Installation

1. Téléchargez la dernière version depuis la section [Releases](https://github.com/o2Cloud-fr/Explorer/releases)
2. Exécutez le fichier Explorer.exe
3. Aucune installation nécessaire - l'application est portable

## 📚 Fonctionnement

Explorer utilise l'API Windows (via P/Invoke) pour accéder aux icônes système et fournir une expérience native. L'application est structurée autour de :

- Un **TreeView** pour la navigation hiérarchique des dossiers
- Un **ListView** pour l'affichage détaillé du contenu
- Un **SplitContainer** pour une interface redimensionnable
- Des appels natifs à **shell32.dll** pour récupérer les icônes système

## 👨‍💻 Auteurs

- [@o2Cloud-fr](https://www.github.com/o2Cloud-fr)

## 🔖 Badges

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![Windows](https://img.shields.io/badge/Platform-Windows-0078D6?logo=windows)](https://github.com/o2Cloud-fr/Explorer)
[![C#](https://img.shields.io/badge/Language-CSharp-239120?logo=c-sharp)](https://github.com/o2Cloud-fr/Explorer)
[![WinForms](https://img.shields.io/badge/Framework-WinForms-512BD4)](https://github.com/o2Cloud-fr/Explorer)

## 🤝 Contribution

Les contributions sont toujours les bienvenues !
N'hésitez pas à ouvrir une issue ou à proposer une pull request pour améliorer cette application.

## 💬 Feedback

Si vous avez des commentaires ou des suggestions, n'hésitez pas à ouvrir une issue sur notre dépôt GitHub.

## 🔗 Liens

[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/remi-simier-2b30142a1/)
[![github](https://img.shields.io/badge/github-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/o2Cloud-fr)

## 🛠️ Compétences

- C#
- Windows Forms
- P/Invoke et interopérabilité Windows
- Manipulation du système de fichiers

## 📝 Licence

[MIT License](https://opensource.org/licenses/MIT)

## 🗺️ Feuille de route

- Ajouter un champ de recherche
- Implémenter la fonction copier/coller de fichiers
- Ajouter la prévisualisation des fichiers images
- Support des opérations de glisser-déposer
- Personnalisation des colonnes d'affichage

## 🆘 Support

Pour obtenir de l'aide, ouvrez une issue sur notre dépôt GitHub ou contactez-nous par email : github@o2cloud.fr

## 💼 Utilisé par

Cette application est idéale pour :
- Les utilisateurs cherchant une alternative légère à l'Explorateur Windows
- Les développeurs souhaitant un exemple d'application de gestion de fichiers en C#
- Les administrateurs système nécessitant un explorateur de fichiers portable
