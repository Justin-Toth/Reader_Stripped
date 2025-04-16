import requests
import sys

def fetch_courses(term):
    url = "https://one.ufl.edu/apix/soc/schedule/"
    params = {
        "category": "CWSP",
        "term": term,
        "last-control-number": 0
    }

    course_results = []
    total_sections = 0

    while True:
        response = requests.get(url, params=params)

        if response.status_code == 200:
            data = response.json()
            courses = data[0].get('COURSES', [])
            retrievedrows = data[0].get('RETRIEVEDROWS', 0)
            lastcontrolnumber = data[0].get('LASTCONTROLNUMBER', 0)

            if retrievedrows == 0 and lastcontrolnumber == 0:
                break

            for course in courses:
                course_code = course.get('code')
                course_name = course.get('name')
                sections = course.get('sections', [])

                for section in sections:
                    class_number = section.get('classNumber')
                    instructors = ', '.join([instructor['name'] for instructor in section['instructors']])
                    for time in section['meetTimes']:
                        meetNum = time['meetNo']
                        meet_time_begin = time['meetTimeBegin']
                        meet_time_end = time['meetTimeEnd']
                        roomCode = time['meetBuilding'] + str(time['meetRoom'])
                        meetDays = time['meetDays']

                        course_results.append({
                            'code': course_code,
                            'classNumber': class_number,
                            'instructor(s)': instructors,
                            'meetNo': meetNum,
                            'meetDays': meetDays,
                            'meetTimeBegin': meet_time_begin,
                            'meetTimeEnd': meet_time_end,
                            'meetRoomCode': roomCode,
                            'name': course_name
                        })

                        total_sections += 1
                        sys.stdout.write(f"\rSections loaded: {total_sections}")
                        sys.stdout.flush()

            params['last-control-number'] = lastcontrolnumber
        else:
            print(f"Failed to retrieve data: {response.status_code}")
            break

    print()
    return course_results

  
def escape_single_quotes(value):
    # Escape single quotes by doubling them
    return value.replace("'", "''")

def remove_semicolons(value):
    # Remove semicolons from the string
    return value.replace(";", "")

def generate_course_sql(course_data, sql_filename):
    lines = []
    lines.append("DROP TABLE IF EXISTS courses;\n")
    lines.append("""
CREATE TABLE courses (
    course_code VARCHAR(20),
    course_name TEXT,
    class_number VARCHAR(20),
    instructors TEXT,
    meet_no VARCHAR(10),
    meet_days TEXT,
    meet_time_begin VARCHAR(10),
    meet_time_end VARCHAR(10),
    meet_room_code VARCHAR(20),
    PRIMARY KEY (class_number, meet_no)
);\n
""")

    count = 0
    seen = set()
    for course in course_data:
        key = (course["classNumber"], course["meetNo"])
        if key in seen:
            continue
        seen.add(key)
        count += 1

        # Remove semicolons and escape single quotes from all fields
        course_code = escape_single_quotes(remove_semicolons(course["code"]))
        course_name = escape_single_quotes(remove_semicolons(course["name"]))
        class_number = course["classNumber"]
        instructors = escape_single_quotes(remove_semicolons(course["instructor(s)"]))
        
        # Convert meetDays list to a string, remove semicolons, and escape single quotes
        meet_days = ', '.join(course["meetDays"]).replace(";", "").replace("'", "''") if isinstance(course["meetDays"], list) else str(course["meetDays"]).replace(";", "").replace("'", "''")
        
        # Remove semicolons and escape single quotes from time-related fields
        meet_time_begin = escape_single_quotes(remove_semicolons(course["meetTimeBegin"]))
        meet_time_end = escape_single_quotes(remove_semicolons(course["meetTimeEnd"]))
        meet_room_code = escape_single_quotes(remove_semicolons(course["meetRoomCode"]))

        # Generate the SQL insert statement
        insert_stmt = f"""INSERT INTO courses (course_code, course_name, class_number, instructors, meet_no, meet_days, meet_time_begin, meet_time_end, meet_room_code)
VALUES ('{course_code}', '{course_name}', '{class_number}', '{instructors}', '{course["meetNo"]}', '{meet_days}', '{meet_time_begin}', '{meet_time_end}', '{meet_room_code}');\n"""
        lines.append(insert_stmt)

    # Write the SQL statements to a file
    with open(sql_filename, "w", encoding="utf-8") as f:
        f.writelines(lines)

    print(f"\nTotal course entries to insert: {count}")
    return count


def normalize_name(name):
    # Normalize course names for consistency in comparison
    return remove_semicolons(name.strip().lower())

def generate_exam_sql(course_data, sql_filename):
    lines = []
    lines.append("DROP TABLE IF EXISTS exams;\n")
    lines.append("""
CREATE TABLE exams (
    course_code VARCHAR(20),
    course_name VARCHAR(255),
    instructors TEXT,
    sections TEXT,
    room TEXT,
    date TEXT,
    start_time TEXT,
    end_time TEXT,
    PRIMARY KEY (course_code, course_name)
);\n
""")

    course_dict = {}

    for course in course_data:
        normalized_key = (course["code"], normalize_name(course["name"]))
        if normalized_key not in course_dict:
            course_dict[normalized_key] = {
                "original_name": course["name"],  # preserve original case for insert
                "instructors": set(),
                "sections": set()
            }

        course_dict[normalized_key]["instructors"].add(course["instructor(s)"])
        course_dict[normalized_key]["sections"].add(course["classNumber"])

    for (code, _), data in course_dict.items():
        original_name = escape_single_quotes(remove_semicolons(data["original_name"]))
        instructors = escape_single_quotes(remove_semicolons(', '.join(sorted(data["instructors"]))))
        sections = escape_single_quotes(remove_semicolons(', '.join(sorted(str(s) for s in data["sections"]))))

        insert_stmt = f"""INSERT INTO exams (course_code, course_name, instructors, sections)
VALUES ('{escape_single_quotes(code)}', '{original_name}', '{instructors}', '{sections}');\n"""
        lines.append(insert_stmt)

    with open(sql_filename, "w", encoding="utf-8") as f:
        f.writelines(lines)

    print(f"Total exam entries to insert: {len(course_dict)}")
    return len(course_dict)
  


def main():
    term = input("Enter the term (e.g., 1, 6W1, 6W2): ")
    course_sql = f"courses_{term}.sql"
    exam_sql = f"exams_{term}.sql"

    course_data = fetch_courses(term)

    course_count = generate_course_sql(course_data, course_sql)
    exam_count = generate_exam_sql(course_data, exam_sql)

    print(f"\nSQL files generated:\n- {course_sql}\n- {exam_sql}")

if __name__ == "__main__":
    main()