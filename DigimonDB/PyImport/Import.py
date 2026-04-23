import json
import sqlite3
from pathlib import Path
import glob

ROOT = Path(__file__).resolve().parents[1]
DATA_DIR = ROOT / "Data"
DB_PATH = ROOT / "digimon.db"

def load_json(path):
    with open(path, "r", encoding="utf-8") as f:
        return json.load(f)

def import_digimons(conn):
    cur = conn.cursor()

    # Read all chunk files and sort by first number in filename.
    files = glob.glob(str(DATA_DIR / "digimon_*-*.json"))
    files.sort(key=lambda p: int(Path(p).stem.split("_")[1].split("-")[0]))

    inserted = 0
    skipped = 0

    for file_path in files:
        payload = load_json(file_path)
        digimons = payload.get("digimons", [])
        for d in digimons:
            # Idempotent insert by Name (same behavior as C# import logic).
            cur.execute("SELECT 1 FROM Digimons WHERE Name = ?", (d["name"],))
            if cur.fetchone():
                skipped += 1
                continue

            cur.execute(
                """
                INSERT INTO Digimons (Name, Type, Level, HP, ATK, DEF, SPD, INT, Description)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)
                """,
                (
                    d.get("name", ""),
                    d.get("type", ""),
                    int(d.get("level", 1)),
                    int(d.get("hp", 0)),
                    int(d.get("atk", 0)),
                    int(d.get("def", 0)),
                    int(d.get("spd", 0)),
                    int(d.get("int", 0)),
                    d.get("description", "")
                )
            )
            inserted += 1

    conn.commit()
    print(f"Digimon import summary: inserted={inserted}, skipped={skipped}")

def import_items(conn):
    cur = conn.cursor()
    items_file = DATA_DIR / "items.json"
    if not items_file.exists():
        print("items.json not found, skipping items.")
        return

    payload = load_json(items_file)
    items = payload.get("items", [])

    inserted = 0
    skipped = 0

    for it in items:
        name = it.get("item", "")
        item_type = it.get("type", "")
        materials = it.get("materials", [])
        description = ", ".join(materials) if isinstance(materials, list) else str(materials)

        cur.execute("SELECT 1 FROM Items WHERE Name = ?", (name,))
        if cur.fetchone():
            skipped += 1
            continue

        cur.execute(
            """
            INSERT INTO Items (Name, Type, Description)
            VALUES (?, ?, ?)
            """,
            (name, item_type, description)
        )
        inserted += 1

    conn.commit()
    print(f"Item import summary: inserted={inserted}, skipped={skipped}")

def main():
    if not DB_PATH.exists():
        raise FileNotFoundError(f"Database not found: {DB_PATH}")

    conn = sqlite3.connect(DB_PATH)
    try:
        import_digimons(conn)
        import_items(conn)
    finally:
        conn.close()

    print("Python import finished.")

if __name__ == "__main__":
    main()