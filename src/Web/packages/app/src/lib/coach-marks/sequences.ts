import type { SequenceConfig } from "@nocturne/coach";

export const sequences: SequenceConfig = {
  onboarding: {
    priority: 100,
    steps: [
      "onboarding.patient-details",
      "onboarding.devices",
      "onboarding.insulins",
      "onboarding.alerts",
      "onboarding.sharing",
      "onboarding.therapy-profile",
    ],
  },
  "dashboard-discovery": {
    priority: 75,
    prerequisite: "onboarding",
    steps: [
      "dashboard-discovery.chart-timerange",
      "dashboard-discovery.widgets",
      "dashboard-discovery.sidebar-glucose",
      "dashboard-discovery.reports",
    ],
  },
  "feature-intro": {
    priority: 50,
    prerequisite: "onboarding",
    steps: [
      "feature-intro.calendar-views",
      "feature-intro.calendar-trackers",
      "feature-intro.meals-attribution",
      "feature-intro.meals-matching",
      "feature-intro.food-quickpicks",
      "feature-intro.appearance-widgets",
    ],
  },
  "power-user": {
    priority: 25,
    prerequisite: "onboarding",
    steps: [
      "power-user.trackers",
      "power-user.connectors",
      "power-user.alert-rules",
      "power-user.compatibility",
    ],
  },
};
