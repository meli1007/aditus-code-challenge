# Code-Challenge

## Voraussetzungen

- .NET6-SDK

## Kontext

Das Projekt beeinhaltet eine REST-HTTP-Schnittstelle, die Informationen über Veranstaltungen bereitstellt.
Es gibt zwei Endpunkte:

- GET /events/ - Liefert eine Liste an Veranstaltungen.
- GET /events/:id - Liefert die Informationen eines bestimmten Events.

Zu einer jeden Veranstaltung liegen folgende Informationen vor:

Information | Beschreibung
----------- | ------------ 
ID          | Kennung der Veranstaltung  
Jahr        | Jahr der Veranstaltung 
Name        | Name der Veranstaltung 
StartDate   | Startdatum der Veranstaltung 
EndDate     | Enddatum der Veranstaltung 
Type        | Typ der Veranstaltung (mögliche Werte: OnSite, Online, Hybrid) 

## Aufgabenstellung

Der Endpunkt zum Abrufen einer Veranstaltung soll um die Ermittlung und Ausgaben von Statistiken der jeweiligen Veranstaltung erweitert werden, hierbei gilt es eine sinnvolle Struktur für die Integration in das bestehende Schnittstellen-Schema zu wählen.

Je nach Event-Typ müssen unterschiedliche Datenquellen für den Abruf der Statistiken verwendet werden. Sollte es sich um eine hybride Veranstaltung handeln, müssen beide Datenquellen herangezogen werden.

Datenquelle                             | URL
--------------------------------------- | ------------------------ 
Statistiken für Online-Veranstaltungen  | GET https://onlineevent-statistics.azurewebsites.net/api/statistics/:eventId
Statistiken für Vor-Ort-Veranstaltungen | GET https://onsite-eventstatistics.azurewebsites.net/api/:id


