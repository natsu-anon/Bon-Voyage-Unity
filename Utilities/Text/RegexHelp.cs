using System;
using System.Text.RegularExpressions;

namespace TextUtil {

public class RegexHelp {
    enum Status { Pending, Failure, Success };
    Regex regex;
    Match match;
    string line;
    Status status;
    Action<Match> action;
    Action<string> fallback;
    bool matchAll;


    /*  CONSTRUCTORS  */


    public RegexHelp (string pattern) : this(new Regex(pattern)) {}


    public RegexHelp (Regex regex) {
        this.regex = regex;
        status = Status.Pending;
    }


    /*  METHODS  */


    public RegexHelp Clone () {
        return new RegexHelp(regex);
    }

    public string Replace (string replacement) {
        return regex.Replace(line, replacement);
    }

    public void Match (string line) {
        this.line = line;
        status = regex.IsMatch(line) ? Status.Success : Status.Failure;
        switch (status) {
            case Status.Success:
                if (action != null) {
                    action(regex.Match(line));
                }
                else {
                    match = regex.Match(line);
                }
                break;
            case Status.Failure:
                if (fallback != null) {
                    fallback(line);
                }
                break;
        }
    }

    public RegexHelp Then (Action<Match> action) {
        switch (status) {
            case Status.Success:
                action(match);
                status = Status.Pending;
                break;
            case Status.Pending:
                this.action = action;
                break;
        }
        return this;
    }

    public RegexHelp Fallback  (Action<string> fallback) {
        switch (status) {
            case Status.Failure:
                fallback(line);
                status = Status.Pending;
                break;
            case Status.Pending:
                this.fallback = fallback;
                break;
        }
        return this;
    }
}

}
