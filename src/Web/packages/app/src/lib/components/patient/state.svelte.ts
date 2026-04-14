import * as patientRemote from "$api/generated/patientRecords.generated.remote";
import { getCatalog as getInsulinCatalog } from "$api/generated/insulinCatalogs.generated.remote";
import { getBodyWeights, create as createBodyWeight } from "$api/generated/bodyWeights.generated.remote";
import {
  type PatientDevice,
  type PatientInsulin,
  type InsulinFormulation,
  DiabetesType,
} from "$api";

/** Reactive clinical form state bound to the patient record API */
export class ClinicalState {
  diabetesType = $state("");
  diabetesTypeOther = $state("");
  diagnosisDate = $state("");
  dateOfBirth = $state("");
  preferredName = $state("");
  pronouns = $state("");
  saving = $state(false);
  saveError = $state<string | null>(null);

  readonly #record = patientRemote.getPatientRecord();

  get isValid() { return !!this.diabetesType; }

  constructor() {
    $effect(() => {
      const r = this.#record.current;
      if (r) {
        this.diabetesType = r.diabetesType ?? "";
        this.diabetesTypeOther = r.diabetesTypeOther ?? "";
        this.diagnosisDate = r.diagnosisDate
          ? new Date(r.diagnosisDate).toISOString().split("T")[0]
          : "";
        this.dateOfBirth = r.dateOfBirth
          ? new Date(r.dateOfBirth).toISOString().split("T")[0]
          : "";
        this.preferredName = r.preferredName ?? "";
        this.pronouns = r.pronouns ?? "";
      }
    });
  }

  save = async (): Promise<boolean> => {
    this.saving = true;
    this.saveError = null;
    try {
      const current = this.#record.current;
      await patientRemote.updatePatientRecord({
        id: current?.id,
        avatarUrl: current?.avatarUrl,
        createdAt: current?.createdAt instanceof Date ? current.createdAt.toISOString() : current?.createdAt,
        modifiedAt: current?.modifiedAt instanceof Date ? current.modifiedAt.toISOString() : current?.modifiedAt,
        diabetesType: (this.diabetesType as DiabetesType) || undefined,
        diabetesTypeOther:
          this.diabetesType === DiabetesType.Other
            ? this.diabetesTypeOther
            : undefined,
        diagnosisDate: this.diagnosisDate || undefined,
        dateOfBirth: this.dateOfBirth || undefined,
        preferredName: this.preferredName || undefined,
        pronouns: this.pronouns || undefined,
      });
      return true;
    } catch {
      this.saveError = "Something went wrong. Please try again.";
      return false;
    } finally {
      this.saving = false;
    }
  };
}

/** Reactive device list state with CRUD */
export class DeviceListState {
  readonly #devices = patientRemote.getDevices();
  readonly createForm = patientRemote.createDevice;
  readonly updateForm = patientRemote.updateDevice;

  get items(): PatientDevice[] { return (this.#devices.current ?? []) as PatientDevice[]; }

  remove = async (id: string): Promise<void> => {
    await patientRemote.deleteDevice(id);
  };
}

/** Reactive insulin list state with CRUD and catalog */
export class InsulinListState {
  readonly #insulins = patientRemote.getInsulins();
  readonly #catalog = getInsulinCatalog(undefined);
  readonly createForm = patientRemote.createInsulin;
  readonly updateForm = patientRemote.updateInsulin;

  get items(): PatientInsulin[] { return (this.#insulins.current ?? []) as PatientInsulin[]; }
  get catalog(): InsulinFormulation[] { return (this.#catalog.current ?? []) as InsulinFormulation[]; }

  remove = async (id: string): Promise<void> => {
    await patientRemote.deleteInsulin(id);
  };
}

/** Reactive weight state for initial body weight entry */
export class WeightState {
  weightKg = $state("");
  saving = $state(false);
  saveError = $state<string | null>(null);

  readonly #existing = getBodyWeights({ count: 1, skip: 0 });

  constructor() {
    $effect(() => {
      const records = this.#existing.current;
      if (records && records.length > 0) {
        this.weightKg = String(records[0].weightKg ?? "");
      }
    });
  }

  save = async (): Promise<boolean> => {
    if (!this.weightKg) return true;
    this.saving = true;
    this.saveError = null;
    try {
      await createBodyWeight({
        weightKg: Number(this.weightKg),
        mills: Date.now(),
      });
      return true;
    } catch {
      this.saveError = "Failed to save weight. Please try again.";
      return false;
    } finally {
      this.saving = false;
    }
  };
}
