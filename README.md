# ADITUS Code-Challenge

## Voraussetzungen

- .NET8-SDK
- VSCode oder Visual Studio

## Bearbeitungshinweis

* Git-Repository clonen
* Repository in VSCode oder Visual Studio öffnen
* Aufgaben bearbeiten
* Nach Bearbeitung die Lösung in einem eigenen Git-Repository bereitstellen
* Einen Link zum Repository per E-Mail dem Ansprechpartner schicken

## Kontext

Das Projekt beeinhaltet eine REST-HTTP-Schnittstelle, die Informationen über Veranstaltungen bereitstellt.
Es gibt zwei Endpunkte:

- GET /events/ - Liefert eine Liste an Veranstaltungen.
- GET /events/:id - Liefert die Informationen einer bestimmten Veranstaltung.

Zu einer jeden Veranstaltung liegen folgende Informationen vor:

Information | Beschreibung
----------- | ------------ 
ID          | Kennung der Veranstaltung  
Year        | Jahr der Veranstaltung 
Name        | Name der Veranstaltung 
StartDate   | Startdatum der Veranstaltung 
EndDate     | Enddatum der Veranstaltung 
Type        | Typ der Veranstaltung (mögliche Werte: OnSite, Online, Hybrid) 

## Aufgabenstellung

### 1. Veranstaltungs-Statistiken

Der bestehende Endpunkt zum Abrufen einer Veranstaltung soll um die Ermittlung und anschließender Ausgabe von Statistiken erweitert werden. 

Zur Ermittlung der Statistiken einer Veranstaltung müssen Datenquellen angebunden werden. Diese Datenquellen können per HTTP-Schnittstelle angesprochen werden.
Je nach Typ der Veranstaltung können folgende URLs angefragt werden, um die Statistiken abzurufen:

Datenquelle                             | URL
--------------------------------------- | ------------------------ 
Statistiken für Online-Veranstaltungen  | GET https://codechallenge-statistics.azurewebsites.net/api/online-statistics/:eventId
Statistiken für Vor-Ort-Veranstaltungen | GET https://codechallenge-statistics.azurewebsites.net/api/onsite-statistics/:eventId

Für den Fall, dass eine Veranstaltung vom Typ "Hybrid" ist, müssen die Statistiken von beiden Datenquellen abgerufen werden.

*Hinweis: Damit die Datenquellen funktionieren, muss als Event-ID eine gültige ID im GUID-Format übermittelt werden. (Beispiel: https://codechallenge-statistics.azurewebsites.net/api/online-statistics/b4b9236b-69bc-4ce3-b923-592786e9c881 bzw. https://codechallenge-statistics.azurewebsites.net/api/onsite-statistics/b4b9236b-69bc-4ce3-b923-592786e9c881)*

### 2. Hardware-Reservierung einer Veranstaltung

Die Schnittstelle soll um einen Prozess zur Reservierung von Hardware zur Zutrittskontrolle erweitert werden.
Für eine Hardware-Reservierung muss mitgeteilt werden, welche Hardware-Komponenten in welcher Menge für die Veranstaltung benötigt werden.
Die Reservierung der Hardware ist nur möglich, wenn die Reservierung mindestens 4 Woche vor Veranstaltungsdurchführung getätigt wird, die gewünschte Hardware in ausreichender Menge verfügbar ist und noch keine Hardware für die gewählte Veranstaltung reserviert wurde. Nachdem die Hardware-Reservierung angefragt wurde, steht noch eine Freigabe der Reservierung aus.

Die Schnittstellen-Konsumenten sollen jederzeit den Status des aktuellen Prozesses und die reservierten Hardware-Komponenten mitsamt der reservierten Menge abfragen können.

Folgende Hardware-Komponenten stehen zur Verfügung:

* Drehsperre
* Funkhandscanner
* Mobiles Scan-Terminal

*Hinweis: Die Implementierung der Hardware-Reservierung muss nur mock-artig / dummyhaft geschehen.*

### 3. Dokumentation der API-Endpunkte (optional)

Neben der implementierten Schnittstelle wird auch eine Dokumentation dieser Schnittstelle benötigt.
Diese soll den potenziellen Konsumenten der Schnittstellen einen Überblick über alle verfügbaren Endpunkte geben.
Zu jedem dieser Endpunkte soll dokumentiert sein, welche Daten der Endpunkt vom Konsumenten entgegennimmt und welche Daten von der Schnittstelle übertragen werden.

Wie die Dokumentation der Schnittstelle erfolgt, kann selbst festgelegt werden.
