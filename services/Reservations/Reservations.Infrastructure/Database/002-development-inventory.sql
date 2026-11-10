INSERT INTO room_types (id, name, max_guests)
VALUES ('standard', 'Standard', 2), ('deluxe', 'Deluxe', 3), ('suite', 'Suite', 4)
ON CONFLICT (id) DO NOTHING;

INSERT INTO nightly_inventory (room_type_id, stay_date, nightly_rate, rooms_total, rooms_reserved)
SELECT room.id, CURRENT_DATE + day_offset, room.rate, room.capacity,
       CASE WHEN room.id = 'suite' AND day_offset = 7 THEN room.capacity ELSE 0 END
FROM (VALUES ('standard', 125.00, 4), ('deluxe', 165.00, 2), ('suite', 245.00, 1)) AS room(id, rate, capacity)
CROSS JOIN generate_series(0, 179) AS days(day_offset)
ON CONFLICT (room_type_id, stay_date) DO NOTHING;
