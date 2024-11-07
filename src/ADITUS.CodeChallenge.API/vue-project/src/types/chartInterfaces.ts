import type { ChartData } from 'chart.js';

// Interface für die Events
export interface MyEvent {
  id: string;
  year: number;
  name: string;
  type: string;
  startDate: string;
  endDate: string;
}

// Interface für Online-Events
export interface OnlineEvent {
  id: string;
  type: string;
  attendees: number;
  invites: number;
  visits: number;
  virtualRooms: number;
}

// Interface für Onsite-Events
export interface OnsiteEvent {
  id: string;
  type: string;
  visitorsCount: number;
  exhibitorsCount: number;
  boothsCount: number;
}

// Interface für Hyrbid-Events
export interface HybridEvent {
  id: string;
  type: string;
  attendees: number;
  invites: number;
  visits: number;
  virtualRooms: number;
  visitorsCount: number;
  exhibitorsCount: number;
  boothsCount: number;
}
