/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO [Article](Nom,Description,Categorie,Quantite,Image,Prix,Poids,Taille,Provenance,Fournisseur,MotsCles)
VALUES ('Ordinateur Portable HP Pavilion',
    'L''ordinateur portable HP Pavilion est équipé d''un processeur Intel Core i5, de 8 Go de RAM et d''un disque dur SSD de 512 Go. Il dispose d''un écran Full HD de 15,6 pouces et est idéal pour le travail et le divertissement.',
    'Informatique',
    15,
    'https://example.com/images/hp_pavilion.jpg',
    799.99,
    1.8,
    15.6,
    'États-Unis',
    'HP Inc.',
    'Ordinateur portable, HP, Intel Core i5, SSD, Full HD, Windows 10')

INSERT INTO [Article](
    [Nom],
    [Description],
    [Categorie],
    [Quantite],
    [Image],
    [Prix],
    [Poids],
    [Provenance],
    [Fournisseur],
    [MotsCles])
VALUES
('Souris Gaming Logitech G Pro',
    'La souris gaming Logitech G Pro est conçue pour offrir une précision et une réactivité exceptionnelles. Elle dispose de boutons programmables et d''un capteur optique avancé.',
    'Informatique',
    50,
    'https://example.com/images/logitech_gpro_mouse.jpg',
    69.99,
    0.12,
    'Suisse',
    'Logitech',
    'Souris gaming, Logitech, Capteur optique, Boutons programmables'
);


INSERT INTO [Article]
(
    [Nom],
    [Description],
    [Categorie],
    [Quantite],
    [Image],
    [Prix],
    [Poids],
    [Provenance],
    [Fournisseur],
    [MotsCles]
)
VALUES
(
    'Casque Audio Sony WH-1000XM4',
    'Le casque audio Sony WH-1000XM4 offre un son haute résolution et une réduction de bruit active exceptionnelle. Il est confortable et idéal pour les longues sessions d''écoute.',
    'Audio',
    40,
    'https://example.com/images/sony_wh1000xm4_headphones.jpg',
    349.99,
    0.25,
    'Japon',
    'Sony Corporation',
    'Casque audio, Sony, Réduction de bruit, Bluetooth, Hi-Res Audio'
);