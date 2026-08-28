import json
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
DATA_DIR = ROOT / "Data"
SOURCE = DATA_DIR / "sample-data.json"

def save_json(path: Path, payload: dict):
    path.write_text(json.dumps(payload, indent=2, ensure_ascii=False), encoding="utf-8")

def main():
    if not SOURCE.exists():
        raise FileNotFoundError(f"Missing source file: {SOURCE}")

    data = json.loads(SOURCE.read_text(encoding="utf-8"))

    digimons = data.get("digimons", [])
    items = data.get("items", [])

    if not digimons:
        raise ValueError("No digimons found in sample-data.json")

    # Split digimons in 50-size ranges, last one naturally becomes 451-475.
    step = 50
    for start in range(1, len(digimons) + 1, step):
        end = min(start + step - 1, len(digimons))
        chunk = digimons[start - 1:end]
        out_path = DATA_DIR / f"digimon_{start}-{end}.json"
        save_json(out_path, {"digimons": chunk})
        print(f"Wrote {out_path.name}: {len(chunk)} digimons")

    # Keep item note if present in source.
    item_payload = {
        "items_note": data.get("items_note", ""),
        "items": items
    }
    save_json(DATA_DIR / "items.json", item_payload)
    print(f"Wrote items.json: {len(items)} items")

if __name__ == "__main__":
    main()