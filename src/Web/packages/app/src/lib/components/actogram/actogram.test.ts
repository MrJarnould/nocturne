import { describe, it, expect } from 'vitest';
import { sliceIntoRows, sliceBgIntoRows, type ActogramPoint, type GlucosePoint } from './actogram';

describe('sliceIntoRows', () => {
	const days = [
		new Date('2026-04-20T00:00:00'),
		new Date('2026-04-21T00:00:00'),
		new Date('2026-04-22T00:00:00'),
	];

	it('assigns a point to the correct row primary window', () => {
		const data: ActogramPoint[] = [
			{ mills: new Date('2026-04-21T10:00:00').getTime() },
		];
		const rows = sliceIntoRows(data, days);
		const apr21Row = rows.find((r) => r.day.getTime() === days[1].getTime());
		expect(apr21Row?.data).toHaveLength(1);
		expect(apr21Row?.data[0].hoursFromStart).toBeCloseTo(10);
	});

	it('assigns a point to the previous row extended window', () => {
		const data: ActogramPoint[] = [
			{ mills: new Date('2026-04-21T10:00:00').getTime() },
		];
		const rows = sliceIntoRows(data, days);
		const apr20Row = rows.find((r) => r.day.getTime() === days[0].getTime());
		expect(apr20Row?.data).toHaveLength(1);
		expect(apr20Row?.data[0].hoursFromStart).toBeCloseTo(34);
		expect(apr20Row?.data[0].isExtended).toBe(true);
	});

	it('returns one row per day in input order', () => {
		const rows = sliceIntoRows([], days);
		expect(rows).toHaveLength(3);
		expect(rows[0].day).toEqual(days[0]);
		expect(rows[2].day).toEqual(days[2]);
	});

	it('handles empty data', () => {
		const rows = sliceIntoRows([], days);
		expect(rows.every((r) => r.data.length === 0)).toBe(true);
	});

	it('point at midnight belongs to primary window of that day', () => {
		const data: ActogramPoint[] = [
			{ mills: new Date('2026-04-21T00:00:00').getTime() },
		];
		const rows = sliceIntoRows(data, days);
		const apr21Row = rows.find((r) => r.day.getTime() === days[1].getTime());
		expect(apr21Row?.data.some((d) => d.hoursFromStart === 0 && !d.isExtended)).toBe(true);
	});
});

describe('sliceBgIntoRows', () => {
	const days = [
		new Date('2026-04-20T00:00:00'),
		new Date('2026-04-21T00:00:00'),
		new Date('2026-04-22T00:00:00'),
	];

	it('places a glucose point in the primary window with correct shape', () => {
		const bg: GlucosePoint[] = [
			{ mills: new Date('2026-04-21T06:30:00').getTime(), sgv: 120, color: 'in-range' },
		];
		const rows = sliceBgIntoRows(bg, days);
		const apr21Row = rows.find((r) => r.day.getTime() === days[1].getTime());
		expect(apr21Row?.bgData).toHaveLength(1);
		expect(apr21Row?.bgData[0].hoursFromStart).toBeCloseTo(6.5);
		expect(apr21Row?.bgData[0].isExtended).toBe(false);
		expect(apr21Row?.bgData[0].point.sgv).toBe(120);
		expect(apr21Row?.bgData[0].point.color).toBe('in-range');
	});

	it('double-plots a glucose point in both primary and previous-row extended windows', () => {
		const bg: GlucosePoint[] = [
			{ mills: new Date('2026-04-21T14:00:00').getTime(), sgv: 95, color: 'in-range' },
		];
		const rows = sliceBgIntoRows(bg, days);

		const apr21Row = rows.find((r) => r.day.getTime() === days[1].getTime());
		expect(apr21Row?.bgData).toHaveLength(1);
		expect(apr21Row?.bgData[0].hoursFromStart).toBeCloseTo(14);
		expect(apr21Row?.bgData[0].isExtended).toBe(false);

		const apr20Row = rows.find((r) => r.day.getTime() === days[0].getTime());
		expect(apr20Row?.bgData).toHaveLength(1);
		expect(apr20Row?.bgData[0].hoursFromStart).toBeCloseTo(38);
		expect(apr20Row?.bgData[0].isExtended).toBe(true);
	});
});
