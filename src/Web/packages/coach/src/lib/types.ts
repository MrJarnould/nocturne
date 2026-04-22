export type MarkStatus = "unseen" | "seen" | "dismissed" | "completed";

export interface MarkState {
  id: string;
  markKey: string;
  status: MarkStatus;
  seenAt: string | null;
  completedAt: string | null;
}

export interface CoachMarkAdapter {
  fetchAll: () => Promise<MarkState[]>;
  update: (key: string, status: MarkStatus) => Promise<void>;
}

export interface CoachMarkStep {
  title: string;
  description: string;
}

export interface CoachMarkOptions {
  key: string;
  step?: number;
  title?: string;
  description?: string;
  steps?: CoachMarkStep[];
  action?: { label: string; href: string };
  completed?: boolean;
  priority?: number;
}

export interface SequenceDefinition {
  priority: number;
  steps: string[];
  prerequisite?: string;
}

export interface SequenceConfig {
  [name: string]: SequenceDefinition;
}

export interface CoachMarkProviderOptions {
  adapter: CoachMarkAdapter;
  sequences?: SequenceConfig;
  settleDelay?: number;
  seenDwellMs?: number;
}

export interface MarkRegistration {
  key: string;
  step: number;
  title: string;
  description: string;
  action?: { label: string; href: string };
  completed: boolean;
  priority: number;
  element: HTMLElement;
}
