CREATE TABLE IF NOT EXISTS room_types (
    id text PRIMARY KEY,
    name text NOT NULL,
    max_guests integer NOT NULL CHECK (max_guests > 0)
);

CREATE TABLE IF NOT EXISTS nightly_inventory (
    room_type_id text NOT NULL REFERENCES room_types (id),
    stay_date date NOT NULL,
    nightly_rate numeric(10, 2) NOT NULL CHECK (nightly_rate >= 0),
    rooms_total integer NOT NULL CHECK (rooms_total >= 0),
    rooms_reserved integer NOT NULL DEFAULT 0 CHECK (rooms_reserved BETWEEN 0 AND rooms_total),
    PRIMARY KEY (room_type_id, stay_date)
);
