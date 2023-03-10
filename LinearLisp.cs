using System.Collections.Generic;
using System.Linq;
using System.Symbolics;
using System.Text.RegularExpressions;
namespace System.Symbolics
{
    public sealed class Linear { public readonly int[] Nodes; public readonly object[] Value; public readonly Evaluation[] Wired; public Linear(int[] nodes, object[] value, Evaluation[] wired) { Nodes = nodes; Value = value; Wired = wired; } }
    public interface IEnvironment
    {
        object Get(Symbol symbol);
        bool Knows(Symbol symbol);
        IEnvironment Set(Symbol symbol, object value, bool outerLookup = true);
        IEnvironment Parent { get; }
        Global Global { get; }
    }
    public interface IFormal : IEnvironment
    {
        IEnvironment Set(int index, object value);
    }
    public sealed class Formal0 : IFormal
    {
        public Formal0(IEnvironment parent) => Parent = parent;
        public object Get(Symbol symbol) => Parent.Get(symbol);
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value, bool outerLookup = true) => Parent.Set(symbol, value, outerLookup);
        public IEnvironment Set(int index, object value) => throw new InvalidOperationException();
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public sealed class Formal1 : IFormal
    {
        private Symbol symbol; private object value;
        public Formal1(Symbol[] parameters, IEnvironment parent) { symbol = parameters[0]; Parent = parent; }
        public Formal1(Symbol[] parameters, object[] values, IEnvironment parent) { symbol = parameters[0]; value = values[0]; Parent = parent; }
        public object Get(Symbol symbol) => symbol.Equals(this.symbol) ? value : Parent.Get(symbol);
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value, bool outerLookup = true) { if (symbol.Equals(this.symbol)) { this.value = value; return this; } else return Parent.Set(symbol, value, outerLookup); }
        public IEnvironment Set(int index, object value) { this.value = value; return this; }
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public sealed class Formal2 : IFormal
    {
        private readonly Symbol[] symbols; private readonly object[] values = new object[2];
        public Formal2(Symbol[] parameters, IEnvironment parent) { symbols = parameters; Parent = parent; }
        public Formal2(Symbol[] parameters, object[] values, IEnvironment parent) { symbols = parameters; var i = -1; while (++i < 2) this.values[i] = values[i]; Parent = parent; }
        public object Get(Symbol symbol) { var i = -1; while (++i < 2) if (symbol.Equals(symbols[i])) return values[i]; return Parent.Get(symbol); }
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value, bool outerLookup = true) { var i = 2; while (0 <= --i) { if (symbol.Equals(symbols[i])) { values[i] = value; return this; } } return Parent.Set(symbol, value, outerLookup); }
        public IEnvironment Set(int index, object value) { values[index] = value; return this; }
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public sealed class Formal3 : IFormal
    {
        private readonly Symbol[] symbols; private readonly object[] values = new object[3];
        public Formal3(Symbol[] parameters, IEnvironment parent) { symbols = parameters; Parent = parent; }
        public Formal3(Symbol[] parameters, object[] values, IEnvironment parent) { symbols = parameters; var i = -1; while (++i < 3) this.values[i] = values[i]; Parent = parent; }
        public object Get(Symbol symbol) { var i = -1; while (++i < 3) if (symbol.Equals(symbols[i])) return values[i]; return Parent.Get(symbol); }
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value, bool outerLookup = true) { var i = 3; while (0 <= --i) { if (symbol.Equals(symbols[i])) { values[i] = value; return this; } } return Parent.Set(symbol, value, outerLookup); }
        public IEnvironment Set(int index, object value) { values[index] = value; return this; }
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public sealed class Formal4 : IFormal
    {
        private readonly Symbol[] symbols; private readonly object[] values = new object[4];
        public Formal4(Symbol[] parameters, IEnvironment parent) { symbols = parameters; Parent = parent; }
        public Formal4(Symbol[] parameters, object[] values, IEnvironment parent) { symbols = parameters; var i = -1; while (++i < 4) this.values[i] = values[i]; Parent = parent; }
        public object Get(Symbol symbol) { var i = -1; while (++i < 4) if (symbol.Equals(symbols[i])) return values[i]; return Parent.Get(symbol); }
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value, bool outerLookup = true) { var i = 4; while (0 <= --i) { if (symbol.Equals(symbols[i])) { values[i] = value; return this; } } return Parent.Set(symbol, value, outerLookup); }
        public IEnvironment Set(int index, object value) { values[index] = value; return this; }
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public sealed class Formal5 : IFormal
    {
        private readonly Symbol[] symbols; private readonly object[] values = new object[5];
        public Formal5(Symbol[] parameters, IEnvironment parent) { symbols = parameters; Parent = parent; }
        public Formal5(Symbol[] parameters, object[] values, IEnvironment parent) { symbols = parameters; var i = -1; while (++i < 5) this.values[i] = values[i]; Parent = parent; }
        public object Get(Symbol symbol) { var i = -1; while (++i < 5) if (symbol.Equals(symbols[i])) return values[i]; return Parent.Get(symbol); }
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value, bool outerLookup = true) { var i = 5; while (0 <= --i) { if (symbol.Equals(symbols[i])) { values[i] = value; return this; } } return Parent.Set(symbol, value, outerLookup); }
        public IEnvironment Set(int index, object value) { values[index] = value; return this; }
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public readonly struct Symbol : IEquatable<Symbol>
    {
        public static readonly Symbol Undefined = default, Open = new Symbol(-1), Close = new Symbol(-2), Quote = new Symbol(-3), Self = new Symbol(-4), Let = new Symbol(-5), Lambda = new Symbol(-6), Set = new Symbol(-7), EOF = new Symbol(int.MaxValue); public readonly int Id;
        public Symbol(int id) => Id = id;
        public int Class => 0 < Id ? Id >> 24 : -1;
        public int Index => 0 < Id ? Id & ((1 << 24) - 1) : -Id;
        public bool Equals(Symbol other) => other.Id == Id;
        public override bool Equals(object obj) => obj is Symbol symbol && Equals(symbol);
        public override int GetHashCode() => Id;
        public override string ToString() => $"{{{nameof(Symbol)}({$"{Class}:{(0 < Id ? Index : Id)}"})}}";
    }
    public class SymbolProvider : List<string>
    {
        private class BuiltinComparer : IComparer<string> { public int Compare(string left, string right) => !ReferenceEquals(left, right) || (string.CompareOrdinal(left, right) != 0) ? (left.Length < right.Length ? 1 : -1) : 0; }
        private readonly IDictionary<string, Symbol> symbols = new Dictionary<string, Symbol>();
        public const string Open = "(", Close = ")", Quote = "`", Self= "$", Let = "let", Lambda = "=>", Set = "="; public readonly ISet<string> Builtins = new SortedSet<string>(new BuiltinComparer());
        public SymbolProvider(string[] core = null) { core = core ?? new[] { string.Empty, Open, Close, Quote, Self, Let, Lambda, Set }; for (var at = 0; at < core.Length; at++) Builtin(core[at], out _); }
        public SymbolProvider Builtin(string name, out Symbol builtin) { builtin = Get(name, -1); return this; }
        public Symbol Builtin(string literal, int startAt = -1) { bool Matches(string input, string value, int at) { var from = at; at = 0; while (from < input.Length && at < value.Length && input[from] == value[at]) { from++; at++; } return at == value.Length; } foreach (var name in Builtins) { if (name.Length > 0 && ((startAt >= 0 && Matches(literal, name, startAt)) || literal == name) && symbols[name].Id < 0) { return symbols[name]; } } return Symbol.Undefined; }
        public Symbol Get(string name, int symbolClass = 0) { if (!symbols.TryGetValue(name, out var symbol)) { symbol = new Symbol(symbolClass < 0 ? -symbols.Count : (symbolClass << 24) | symbols.Count); if (symbolClass < 0) Builtins.Add(name); symbols.Add(name, symbol); Add(name); } return symbol; }
        public string NameOf(Symbol symbol) => this[symbol.Index];
    }
    public class Environment : Dictionary<Symbol, object>, IEnvironment
    {
        public static readonly object[] Empty = new object[0];
        public static IEnumerable<object> Sequence(object value, bool required = false) { bool TryGetSequence(object candidate, out IEnumerable<object> sequence) => ((sequence = candidate as object[]) ?? (sequence = candidate as IEnumerable<object>) ?? (sequence = candidate as Collections.IEnumerable != null ? ((Collections.IEnumerable)candidate).Cast<object>() : null)) != null; return TryGetSequence(value, out var found) ? found : !required ? null : throw new Exception($"Not a sequence: {nameof(value)}"); }
        public Environment(IEnvironment parent) => Global = (Parent = parent)?.Global ?? (Global)this;
        public virtual object Get(Symbol symbol) => symbol.Id <= 0 ? (-symbol.Id < Global.Builtins.Count ? Global.Builtins[-symbol.Id] : Symbol.Undefined) : TryGetValue(symbol, out var value) ? value : Parent != null ? Parent.Get(symbol) : Symbol.Undefined;
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public virtual IEnvironment Set(Symbol symbol, object value, bool outerLookup = true) { if (ContainsKey(symbol) || !outerLookup) { this[symbol] = value; return this; } else return Parent.Set(symbol, value, true); }
        public IEnvironment Parent { get; }
        public Global Global { get; }
    }
    public sealed class Global : Environment
    {
        private object[] builtins = new object[3] { null, null, null };
        public Global(SymbolProvider symbolProvider) : base(null) => SymbolProvider = symbolProvider ?? throw new ArgumentNullException(nameof(symbolProvider), "Cannot be null");
        public override object Get(Symbol symbol) => symbol.Id <= 0 ? (-symbol.Id < builtins.Length ? builtins[-symbol.Id] : Symbol.Undefined) : TryGetValue(symbol, out var value) ? value : Symbol.Undefined;
        public override IEnvironment Set(Symbol symbol, object value, bool outerLookup = true) { if (symbol.Id < 0) { var at = -symbol.Id; var upto = at + 1; if (upto > builtins.Length) Array.Resize(ref builtins, upto); builtins[at] = value; } if (0 < symbol.Id) this[symbol] = value; return this; }
        public SymbolProvider SymbolProvider { get; }
        public IReadOnlyList<object> Builtins => builtins;
    }
    public delegate object Inline(IEnvironment site, int at);
    public delegate object Invoke(params object[] arguments);
    public abstract class Closure : LinearEvaluator
    {
        protected readonly Linear linear; public readonly IEnvironment Environment; public readonly Symbol[] Parameters; public readonly int Body;
        protected Closure(IEnvironment environment, Symbol[] parameters, Linear linear, int body) { this.linear = linear; Environment = environment; Parameters = parameters; Body = body; }
        internal abstract object Inline(IEnvironment site, int at, int actuals);
        public abstract object Invoke(params object[] arguments);
    }
    public sealed class Closure0 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals) => Evaluate(new Formal0(Environment), linear, Body);
        public Closure0(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal0(Environment), linear, Body);
    }
    public sealed class Closure1 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals)
        {
            var parameters = Parameters;
            var bound = new Formal1(parameters, Environment);
            if (0 < actuals) bound.Set(0, Evaluate(site, linear, at));
            return Evaluate(bound, linear, Body);
        }
        public Closure1(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal1(Parameters, arguments, Environment), linear, Body);
    }
    public sealed class Closure2 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals)
        {
            var parameters = Parameters;
            var bound = new Formal2(parameters, Environment);
            if (0 < actuals) bound.Set(0, Evaluate(site, linear, at));
            if (1 < actuals) bound.Set(1, Evaluate(site, linear, at += linear.Nodes[at]));
            return Evaluate(bound, linear, Body);
        }
        public Closure2(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal2(Parameters, arguments, Environment), linear, Body);
    }
    public sealed class Closure3 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals)
        {
            var parameters = Parameters;
            var bound = new Formal3(parameters, Environment);
            if (0 < actuals) bound.Set(0, Evaluate(site, linear, at));
            if (1 < actuals) bound.Set(1, Evaluate(site, linear, at += linear.Nodes[at]));
            if (2 < actuals) bound.Set(2, Evaluate(site, linear, at += linear.Nodes[at]));
            return Evaluate(bound, linear, Body);
        }
        public Closure3(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal3(Parameters, arguments, Environment), linear, Body);
    }
    public sealed class Closure4 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals)
        {
            var parameters = Parameters;
            var bound = new Formal4(parameters, Environment);
            if (0 < actuals) bound.Set(0, Evaluate(site, linear, at));
            if (1 < actuals) bound.Set(1, Evaluate(site, linear, at += linear.Nodes[at]));
            if (2 < actuals) bound.Set(2, Evaluate(site, linear, at += linear.Nodes[at]));
            if (3 < actuals) bound.Set(3, Evaluate(site, linear, at += linear.Nodes[at]));
            return Evaluate(bound, linear, Body);
        }
        public Closure4(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal4(Parameters, arguments, Environment), linear, Body);
    }
    public sealed class Closure5 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals)
        {
            var parameters = Parameters;
            var bound = new Formal5(parameters, Environment);
            if (0 < actuals) bound.Set(0, Evaluate(site, linear, at));
            if (1 < actuals) bound.Set(1, Evaluate(site, linear, at += linear.Nodes[at]));
            if (2 < actuals) bound.Set(2, Evaluate(site, linear, at += linear.Nodes[at]));
            if (3 < actuals) bound.Set(3, Evaluate(site, linear, at += linear.Nodes[at]));
            if (4 < actuals) bound.Set(4, Evaluate(site, linear, at += linear.Nodes[at]));
            return Evaluate(bound, linear, Body);
        }
        public Closure5(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal5(Parameters, arguments, Environment), linear, Body);
    }
    public delegate object Evaluation(IEnvironment environment, Linear linear, int at);
    public class LinearEvaluator
    {
        protected static object Rehydrate(Linear linear, int at)
        {
            var nodes = linear.Nodes;
            var it = at;
            if (2 < nodes[it++])
            {
                var length = nodes[it++];
                var array = new object[length];
                var i = 0;
                while (i < length)
                {
                    array[i++] = Rehydrate(linear, it);
                    it += nodes[it];
                }
                return array;
            }
            else
            {
                return (it = nodes[it]) <= 0 ? (it == 0 || linear.Nodes[0] <= -it ? linear.Value[-it] : new Symbol(it)) : new Symbol(it);
            }
        }
        protected static object Evaluate(IEnvironment environment, Linear linear, int at)
        {
            var wired = linear.Wired;
            var evaluate = wired[at];
            if (evaluate != null)
            {
                return evaluate(environment, linear, at);
            }
            var nodes = linear.Nodes;
            var it = at;
            if (2 < nodes[it++])
            {
                var length = nodes[it];
                var suffix = Math.Min(length, 2);
                var value = linear.Value;
                int exp = ++it, arg, bin;
                var i = -1;
                while (++i < suffix)
                {
                    if ((arg = nodes[it]) == 2 && 0 < (bin = -nodes[it + 1]) && bin < nodes[0] && value[bin] != null)
                    {
                        wired[at] = evaluate = (Evaluation)value[bin]; break;
                    }
                    it += arg;
                }
                return
                    evaluate != null ?
                    evaluate(environment, linear, at) :
                    Evaluate(environment, linear, exp) is Closure closure ? closure.Inline(environment, exp += nodes[exp], length - 1) : Rehydrate(linear, at);
            }
            else
            {
                return 0 < (it = nodes[it]) ? environment.Get(new Symbol(it)) : linear.Value[-it];
            }
        }
        public object Rehydrate(Linear linear) => Rehydrate(linear, 1);
        public Linear Linearize(IEnvironment environment, object expression)
        {
            List<int> LinearizeAtom(List<object> v, object a)
            {
                int found;
                if (!(a is Symbol s))
                {
                    found = v.IndexOf(a);
                    if (found < 0)
                    {
                        v.Add(a);
                        found = v.Count - 1;
                    }
                    found = -found;
                }
                else
                {
                    found = s.Id;
                }
                return new List<int> { 2, found };
            }
            List<int> LinearizeArray(List<object> v, object[] a, bool isLambda)
            {
                var length = a.Length;
                var list = new List<int> { 0, length };
                var data = new List<List<int>>();
                var size = list.Count;
                for (var i = 0; i < length; i++)
                {
                    var item = isLambda && i == 0 ? LinearizeAtom(v, ((object[])a[i]).Cast<Symbol>().ToArray()) : Linearize(v, a[i]);
                    data.Add(item);
                    size += item.Count;
                }
                for (var i = 0; i < length; i++) list.AddRange(data[i]);
                list[0] = size;
                return list;
            }
            List<int> Linearize(List<object> v, object o) =>
                o is object[] a ?
                LinearizeArray(v, a, a.Length == 3 && Symbol.Lambda.Equals(a[1]))
                :
                LinearizeAtom(v, o);
            var nodes = new List<int> { environment.Global.Builtins.Count };
            var value = new List<object>(new object[3] { Environment.Empty, null, typeof(void) }.Concat(environment.Global.Builtins.Skip(3).ToArray()));
            nodes.AddRange(Linearize(value, expression));
            return new Linear(nodes.ToArray(), value.ToArray(), new Evaluation[nodes.Count]);
        }
        public object Evaluate(IEnvironment environment, Linear linear) => Evaluate(environment, linear, 1);
    }
    public class Interpreter : LinearEvaluator
    {
        private static readonly Type[] Closures = new Type[6] { typeof(Closure0), typeof(Closure1), typeof(Closure2), typeof(Closure3), typeof(Closure4), typeof(Closure5) };
        protected static object Quotation(IEnvironment environment, Linear linear, int at) => Rehydrate(linear, at + 7);
        protected static object Reflection(IEnvironment environment, Linear linear, int at) => null;
        protected static object Definition(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            environment = new Environment(environment);
            var lets = at += 4;
            var body = at + nodes[at];
            var size = nodes[++lets];
            if (0 < size)
            {
                Symbol symbol = default;
                var i = -1;
                at = ++lets;
                while (++i < size)
                {
                    if ((i % 2) == 0)
                    {
                        symbol = new Symbol(nodes[at + 1]);
                    }
                    else
                    {
                        environment.Set(symbol, Evaluate(environment, linear, at), false);
                    }
                    at += nodes[at];
                }
            }
            return Evaluate(environment, linear, body);
        }
        protected static object Abstraction(IEnvironment environment, Linear linear, int at)
        {

            var nodes = linear.Nodes;
            var parameters = (Symbol[])linear.Value[-nodes[at + 3]];
            return Activator.CreateInstance(Closures[parameters.Length], environment, parameters, linear, at + 6);
        }
        protected static object Assignment(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            object value;
            at += 2;
            if (2 < nodes[at])
            {
                var array = (Array)Evaluate(environment, linear, at + 2);
                var index = Convert.ToInt32(Evaluate(environment, linear, at + 6));
                value = Evaluate(environment, linear, at + nodes[at] + 2);
                array.SetValue(value, index);
            }
            else
            {
                var symbol = new Symbol(nodes[at + 1]);
                value = Evaluate(environment, linear, at + 4);
                environment.Set(symbol, value);
            }
            return value;
        }
        protected virtual object Token(object context, string input, ref int offset, out int length) { length = 0; return Symbol.EOF; }
        protected virtual object Parse(object context, string input, object current, ref int offset, int matched)
        {
            if (Symbol.EOF.Equals(current) || Symbol.Undefined.Equals(current)) throw new Exception($"Unexpected {(Symbol.Undefined.Equals(current) ? $"'{input[offset]}'" : "EOF")} at {offset}");
            else if (Symbol.Quote.Equals(current)) { offset += matched; current = Quoted(Symbol.Quote, Parse(context, input, Token(context, input, ref offset, out matched), ref offset, matched)); }
            else if (Symbol.Open.Equals(current))
            {
                var list = new List<object>(); offset += matched; while (!Symbol.EOF.Equals(current = Token(context, input, ref offset, out matched)) && !Symbol.Undefined.Equals(current) && !Symbol.Close.Equals(current)) list.Add(Parse(context, input, current, ref offset, matched));
                if (!Symbol.EOF.Equals(current) && !Symbol.Undefined.Equals(current)) offset += matched; else throw new Exception($"Unexpected {(Symbol.Undefined.Equals(current) ? $"'{input[offset]}'" : "EOF")} at {offset}"); current = list.ToArray();
            }
            else offset += matched;
            return current;
        }
        protected virtual Global AsGlobal(IEnvironment environment) => (Global)environment.Set(Symbol.Quote, (Evaluation)Quotation).Set(Symbol.Self, (Evaluation)Reflection).Set(Symbol.Let, (Evaluation)Definition).Set(Symbol.Lambda, (Evaluation)Abstraction).Set(Symbol.Set, (Evaluation)Assignment);
        public object Quoted(Symbol quote, object expression) => new object[2] { quote, expression };
        public bool IsQuoted(object expression, out Symbol quote) => (expression is object[] list && list.Length == 2 && Symbol.Quote.Equals(quote = list[0] is Symbol symbol ? symbol : Symbol.Undefined)) || Symbol.Quote.Equals(quote = Symbol.Undefined);
        public object Evaluate(IEnvironment environment, object expression) => base.Evaluate(environment, Linearize(environment, expression));
        public object Evaluate(IEnvironment environment, string input) => Evaluate(environment, input, out _);
        public object Evaluate(IEnvironment environment, string input, out Global global) => Evaluate(environment, input, out global, out _);
        public object Evaluate(IEnvironment environment, string input, out Global global, out object parsed) => Evaluate(global = AsGlobal(environment ?? new Global(GetSymbolProvider(null))), parsed = Parse(global, input));
        public virtual SymbolProvider GetSymbolProvider(object context) => (context as SymbolProvider) ?? (context as Global)?.SymbolProvider;
        public virtual object Parse(object context, string input) { var offset = 0; var expression = Parse(context, input, Token(context, input, ref offset, out var matched), ref offset, matched); return Symbol.EOF.Equals(Token(context, input, ref offset, out _)) ? expression : throw new Exception($"Unexpected '{input[offset]}' at {offset}"); }
        public virtual string Print(object context, object expression) { var symbols = GetSymbolProvider(context); var sequence = Environment.Sequence(expression); var nonEmpty = sequence != null && sequence.Any(); return sequence != null ? (!IsQuoted(expression, out var quotedWith) ? $"( {string.Join('\x20', sequence.Select(value => Print(context, value)))}{(nonEmpty ? " )" : ")")}" : $"{symbols.NameOf(quotedWith)}{Print(context, ((object[])expression)[1])}") : expression is Symbol ? symbols.NameOf((Symbol)expression) : Print(context, symbols.Get($"[:{expression?.GetType() ?? typeof(void)}]")); }
    }
}

class Program
{
    // An interpreter which knows whitespace, signed 64bit integers, and the core lambda calculus (let and lambda forms only),
    // but no bindings of programmer-defined identifiers yet
    public class DerivedInterpreter : Interpreter
    {
        private SymbolProvider symbolProvider;
        protected static readonly Regex WhiteSpace = new Regex("\\s+", RegexOptions.Compiled);
        protected static readonly Regex Integer = new Regex("\\-?(0|[1-9]+[0-9]*)", RegexOptions.Compiled);

        protected override object Token(object context, string input, ref int offset, out int length)
        {
            var symbols = GetSymbolProvider(context);
            Match match;
            length = 0;
            while ((match = WhiteSpace.Match(input, offset)).Success && match.Index == offset) offset += match.Length;
            if (offset < input.Length)
            {
                Symbol builtin;
                if (char.IsDigit(input[offset]) || (input[offset] == '-' && offset < input.Length - 1 && char.IsDigit(input[offset + 1])))
                {
                    match = Integer.Match(input, offset);
                    length = match.Length;
                    return long.Parse(match.Value);
                }
                if ((builtin = symbols.Builtin(input, offset)).Id < 0)
                {
                    length = symbols.NameOf(builtin).Length;
                    return builtin;
                }
                return Symbol.Undefined;
            }
            return Symbol.EOF;
        }

        public override SymbolProvider GetSymbolProvider(object context) => base.GetSymbolProvider(context) ?? symbolProvider ?? (symbolProvider = new SymbolProvider());

        public override string Print(object context, object expression) => expression is int n ? $"{n}" : base.Print(context, expression);
    }

    // An interpreter which knows: whitespace, signed 64bit integers, the five operations (+, -, *, /, and %),
    // the core lambda calculus (let and lambda forms only), the ternary if-then-else conditional operator - ie: ( condition ? ifTrue : ifFalse ) -
    // and lexically-scoped bindings of programmer-defined identifiers, as well as lexically-scoped variable assignments
    // (eg, for impure, side effectful functions);
    // this evaluator thus allows to implement the recursive factorial or Fibonacci sequence function, and more...
    public class LinearLisp : DerivedInterpreter
    {
        private Symbol ellipsis, colon, brackets, pound, atSign, comma, plus, minus, times, divideBy, percent, power, inc, dec, expand, shrink, and, lessThan, lessThanOrEqual, query, @for, @while;
        protected static readonly Regex Literal = new Regex("\"(\\\\\"|[^\"])*\"", RegexOptions.Compiled);
        protected static readonly Regex Identifier = new Regex("[A-Za-z_][A-Za-z_0-9]*", RegexOptions.Compiled);

        protected static object NewList(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            var length = nodes[at + 1] - 1;
            var value = at + 2;
            var token = nodes[at += 4];
            List<object> list;
            if (token == 2 && (token = nodes[at + 1]) < 0 && -token < nodes[0])
            {
                at += 2; int count; list = new List<object>(count = System.Convert.ToInt32(Evaluate(environment, linear, at)));
                var filler = Evaluate(environment, linear, value);
                for (var i = 0; i < count; i++) list.Add(filler);
            }
            else
            {
                var i = 0; list = new List<object>(length);
                while (i < length) { list.Add(Evaluate(environment, linear, at)); at += nodes[at]; i++; }
            }
            return list;
        }

        protected static object ListAdd(IEnvironment environment, Linear linear, int at)
        {
            var left = at += 2;
            var right = at += linear.Nodes[at] + 2;
            var list = (List<object>)Evaluate(environment, linear, left);
            list.Add(Evaluate(environment, linear, right));
            return list;
        }

        protected static object NewArray(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            var length = nodes[at + 1] - 1;
            var value = at + 2;
            var token = nodes[at += 4];
            object[] array;
            if (token == 2 && (token = nodes[at + 1]) < 0 && -token < nodes[0])
            {
                at += 2; array = new object[System.Convert.ToInt32(Evaluate(environment, linear, at))];
                System.Array.Fill(array, Evaluate(environment, linear, value));
            }
            else
            {
                var i = 0; array = new object[length]; while (i < length) { array[i++] = Evaluate(environment, linear, at); at += nodes[at]; }
            }
            return array;
        }

        protected static object CountOf(IEnvironment environment, Linear linear, int at)
        {
            var enumerable = Evaluate(environment, linear, at + 4);
            if (enumerable is System.Array array)
            {
                return array.LongLength;
            }
            else
            {
                var list = (List<object>)enumerable;
                return (long)list.Count;
            }
        }

        protected static object Access(IEnvironment environment, Linear linear, int at)
        {
            at += 2;
            var enumerable = Evaluate(environment, linear, at);
            if (enumerable is System.Array array)
            {
                return array.GetValue(System.Convert.ToInt32(Evaluate(environment, linear, at + 4)));
            }
            else
            {
                var list = (List<object>)enumerable;
                return list[System.Convert.ToInt32(Evaluate(environment, linear, at + 4))];
            }
        }

        protected static object Statements(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            var count = (nodes[at + 1] + 1) / 2;
            object last = null;
            var i = 0;
            at += 2;
            while (i < count)
            {
                last = Evaluate(environment, linear, at);
                at += nodes[at] + 2;
                i++;
            }
            return last;
        }

        protected static object Addition(IEnvironment environment, Linear linear, int at)
        {
            var left = at += 2;
            var right = at += linear.Nodes[at] + 2;
            return (long)Evaluate(environment, linear, left) + (long)Evaluate(environment, linear, right);
        }

        protected static object Subtraction(IEnvironment environment, Linear linear, int at)
        {
            var left = at += 2;
            var right = at += linear.Nodes[at] + 2;
            return (long)Evaluate(environment, linear, left) - (long)Evaluate(environment, linear, right);
        }

        protected static object Multiplication(IEnvironment environment, Linear linear, int at)
        {
            var left = at += 2;
            var right = at += linear.Nodes[at] + 2;
            return (long)Evaluate(environment, linear, left) * (long)Evaluate(environment, linear, right);
        }

        protected static object Division(IEnvironment environment, Linear linear, int at)
        {
            var left = at += 2;
            var right = at += linear.Nodes[at] + 2;
            return (long)Evaluate(environment, linear, left) / (long)Evaluate(environment, linear, right);
        }

        protected static object Modulus(IEnvironment environment, Linear linear, int at)
        {
            var left = at += 2;
            var right = at += linear.Nodes[at] + 2;
            return (long)Evaluate(environment, linear, left) % (long)Evaluate(environment, linear, right);
        }

        protected static object Exponent(IEnvironment environment, Linear linear, int at)
        {
            var left = at += 2;
            var right = at += linear.Nodes[at] + 2;
            return System.Convert.ToInt64(System.Math.Pow((long)Evaluate(environment, linear, left), (long)Evaluate(environment, linear, right)));
        }

        protected static object Increment(IEnvironment environment, Linear linear, int at)
        {
            var symbol = new Symbol(linear.Nodes[at + 3]);
            var number = (long)environment.Get(symbol) + (long)Evaluate(environment, linear, at + 6);
            environment.Set(symbol, number);
            return number;
        }

        protected static object Decrement(IEnvironment environment, Linear linear, int at)
        {
            var symbol = new Symbol(linear.Nodes[at + 3]);
            var number = (long)environment.Get(symbol) - (long)Evaluate(environment, linear, at + 6);
            environment.Set(symbol, number);
            return number;
        }

        protected static object Expanded(IEnvironment environment, Linear linear, int at)
        {
            var symbol = new Symbol(linear.Nodes[at + 3]);
            var number = (long)environment.Get(symbol) * (long)Evaluate(environment, linear, at + 6);
            environment.Set(symbol, number);
            return number;
        }

        protected static object Shrinked(IEnvironment environment, Linear linear, int at)
        {
            var symbol = new Symbol(linear.Nodes[at + 3]);
            var number = (long)environment.Get(symbol) / (long)Evaluate(environment, linear, at + 6);
            environment.Set(symbol, number);
            return number;
        }

        protected static object LogicalAnd(IEnvironment environment, Linear linear, int at)
        {
            var left = at += 2;
            var right = at += linear.Nodes[at] + 2;
            return (bool)Evaluate(environment, linear, left) && (bool)Evaluate(environment, linear, right);
        }

        protected static object IsLessThan(IEnvironment environment, Linear linear, int at)
        {
            var left = at += 2;
            var right = at += linear.Nodes[at] + 2;
            return (long)Evaluate(environment, linear, left) < (long)Evaluate(environment, linear, right);
        }

        protected static object IsLessThanOrEqual(IEnvironment environment, Linear linear, int at)
        {
            var left = at += 2;
            var right = at += linear.Nodes[at] + 2;
            return (long)Evaluate(environment, linear, left) <= (long)Evaluate(environment, linear, right);
        }

        protected static object IfThenElse(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            var test = at += 2;
            var then = at += nodes[at] + 2;
            var @else = at += nodes[at] + 2;
            return (bool)Evaluate(environment, linear, test) ? Evaluate(environment, linear, then) : Evaluate(environment, linear, @else);
        }

        protected static object ForLoop(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            var bound = at += 4;
            var start = at += 4;
            var count = at += nodes[at] + 2;
            var body = at += nodes[at] + 2;
            var iter = new Formal1(new Symbol[1] { new Symbol(nodes[bound + 1]) }, environment);
            var from = (long)Evaluate(environment, linear, start);
            var take = (long)Evaluate(environment, linear, count);
            object last = Symbol.Undefined;
            take += from;
            for (var i = from; i < take; i++) last = Evaluate(iter.Set(0, i), linear, body);
            return last;
        }

        protected static object WhileLoop(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            var test = at += 4;
            var body = at += nodes[at];
            object last = Symbol.Undefined;
            while ((bool)Evaluate(environment, linear, test)) last = Evaluate(environment, linear, body);
            return last;
        }

        protected override object Token(object context, string input, ref int offset, out int length)
        {
            var symbols = GetSymbolProvider(context);
            Match match;
            length = 0;
            while ((match = WhiteSpace.Match(input, offset)).Success && match.Index == offset) offset += match.Length;
            if (offset < input.Length)
            {
                Symbol builtin;
                if (char.IsDigit(input[offset]) || (input[offset] == '-' && offset < input.Length - 1 && char.IsDigit(input[offset + 1])))
                {
                    match = Integer.Match(input, offset);
                    length = match.Length;
                    return long.Parse(match.Value);
                }
                if ((match = Literal.Match(input, offset)).Success && match.Index == offset)
                {
                    length = match.Value.Length;
                    return match.Value.Substring(1, match.Value.Length - 2).Replace("\\\"", "\"");
                }
                if (char.IsLetter(input[offset]) || input[offset] == '_')
                {
                    match = Identifier.Match(input, offset);
                    length = match.Length;
                    return match.Value != "null" ? (match.Value == "false" || match.Value == "true" ? match.Value == "true" : symbols.Get(match.Value)) : null;
                }
                if ((builtin = symbols.Builtin(input, offset)).Id < 0)
                {
                    length = symbols.NameOf(builtin).Length;
                    return builtin;
                }
                return Symbol.Undefined;
            }
            return Symbol.EOF;
        }

        protected override Global AsGlobal(IEnvironment environment) => !base.AsGlobal(environment).Knows(While) ? (Global)environment
            .Set(Ellipsis, (Evaluation)NewList).Set(Colon, (Evaluation)ListAdd).Set(Brackets, (Evaluation)NewArray).Set(Pound, (Evaluation)CountOf).Set(AtSign, (Evaluation)Access).Set(Comma, (Evaluation)Statements)
            .Set(Plus, (Evaluation)Addition).Set(Minus, (Evaluation)Subtraction).Set(Times, (Evaluation)Multiplication).Set(DivideBy, (Evaluation)Division).Set(Percent, (Evaluation)Modulus).Set(Power, (Evaluation)Exponent)
            .Set(Inc, (Evaluation)Increment).Set(Dec, (Evaluation)Decrement).Set(Expand, (Evaluation)Expanded).Set(Shrink, (Evaluation)Shrinked)
            .Set(And, (Evaluation)LogicalAnd).Set(LessThan, (Evaluation)IsLessThan).Set(LessThanOrEqual, (Evaluation)IsLessThanOrEqual).Set(Query, (Evaluation)IfThenElse)
            .Set(For, (Evaluation)ForLoop).Set(While, (Evaluation)WhileLoop)
            :
            (Global)environment;

        public override SymbolProvider GetSymbolProvider(object context) => base.GetSymbolProvider(context)
            .Builtin("...", out ellipsis).Builtin(":", out colon).Builtin("[]", out brackets).Builtin("#", out pound).Builtin("@", out atSign).Builtin(",", out comma)
            .Builtin("+", out plus).Builtin("-", out minus).Builtin("*", out times).Builtin("/", out divideBy).Builtin("%", out percent).Builtin("**", out power)
            .Builtin("+=", out inc).Builtin("-=", out dec).Builtin("*=", out expand).Builtin("/=", out shrink)
            .Builtin("&&", out and).Builtin("<", out lessThan).Builtin("<=", out lessThanOrEqual).Builtin("?", out query)
            .Builtin("for", out @for).Builtin("while", out @while);

        public override string Print(object context, object expression) => expression is string s ? $"\"{s.Replace("\"", "\\\"")}\"" : expression != null ? base.Print(context, expression) : "null";

        public Symbol Ellipsis => ellipsis;

        public Symbol Colon => colon;

        public Symbol Brackets => brackets;

        public Symbol Pound => pound;

        public Symbol AtSign => atSign;

        public Symbol Comma => comma;

        public Symbol Plus => plus;

        public Symbol Minus => minus;

        public Symbol Times => times;

        public Symbol DivideBy => divideBy;

        public Symbol Percent => percent;

        public Symbol Power => power;

        public Symbol Inc => inc;

        public Symbol Dec => dec;

        public Symbol Expand => expand;

        public Symbol Shrink => shrink;

        public Symbol And => and;

        public Symbol LessThan => lessThan;

        public Symbol LessThanOrEqual => lessThanOrEqual;

        public Symbol Query => query;

        public Symbol For => @for;

        public Symbol While => @while;
    }

    static long CompiledFactorial(long n) => 0 < n ? n * CompiledFactorial(n - 1) : 1;

    static long CompiledFib(long n) =>
        n < 2 ? n : CompiledFib(n - 1) + CompiledFib(n - 2);

    static List<long> CompiledGetPrimesBelow(long n)
    {
        var found = new List<long>();
        if (n <= 2) return found;
        var prime = new bool[n];
        for (var i = 2; i < n; i++) prime[i] = true;
        prime[1] = prime[0] = false;
        for (var i = 2; i * i < n; i++)
        {
            if (prime[i]) for (var j = i; j * i < n; j++) prime[j * i] = false;
        }
        for (long p = 0; p < prime.Length; p++) if (prime[p]) found.Add(p);
        return found;
    }

    static void Main(string[] args)
    {
        System.Diagnostics.Stopwatch sw;
        long ms;

        var evaluator = new LinearLisp();

        System.Console.WriteLine("( For history, see also: http://dada.perl.it/shootout/fibo.html )");
        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to start...");
        System.Console.ReadKey(true);

        /*// Cf. https://news.ycombinator.com/item?id=31427506
        // (Python 3.10 vs JavaScript thread)
        const int N = 10_000_000;
        System.Console.WriteLine($"LinearLisp... About to stress the for loop vs lexical scope {N} times...");
        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);
        sw = System.Diagnostics.Stopwatch.StartNew();
        var acc = (long)evaluator.Evaluate(null, $@"(
    let
    (
        N {N}

        acc 0

        f ( ( x ) => ( acc + ( 10 * x ) ) )
    )

    ( for i = 0, N :
        ( acc = ( f i ) )
    )
)");
        sw.Stop();
        ms = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"acc = {acc} ... in {ms} ms");

        System.Diagnostics.Debug.Assert(acc == 499_999_950_000_000);*/

        System.Console.WriteLine();
        System.Console.WriteLine("C# compiled vs LinearLisp, with Fib(32) computed only once, and 20! computed 1,000,000 times...");
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        var result = evaluator.Evaluate(null, @"(
    let
    (
        Fib ( ( n ) => ( ( n < 2 ) ? n : ( ( Fib ( n - 1 ) ) + ( Fib ( n - 2 ) ) ) ) )
    )

    Fib
)  ", out var global);
        var closure = (Closure)result;

        sw = System.Diagnostics.Stopwatch.StartNew();
        var fib32 = CompiledFib(32);
        sw.Stop();
        ms = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"(C# compiled) Fib(32) = {fib32} ... in {ms:0,0} ms");

        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        sw = System.Diagnostics.Stopwatch.StartNew();
        var fib32bis = (long)closure.Invoke(32L);
        sw.Stop();
        ms = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"(LinearLisp) Fib(32) = {fib32bis} ... in {ms:0,0} ms");

        System.Diagnostics.Debug.Assert(fib32 == fib32bis);

        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        const int N_fact = 1_000_000;

        closure = (Closure)evaluator.Evaluate(global, @"(
    let
    (
        Fact ( ( n ) => ( ( 1 < n ) ? ( n * ( Fact ( n - 1 ) ) ) : 1 ) )
    )

    Fact
)");

        sw = System.Diagnostics.Stopwatch.StartNew();
        long fact20 = 0;
        for (var i = 0; i < N_fact; i++)
        {
            fact20 = CompiledFactorial(20);
        }
        sw.Stop();
        var elapsed = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"(C# compiled) 20! = {fact20} x {N_fact:0,0} times ... in {elapsed:0,0} ms");

        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        sw = System.Diagnostics.Stopwatch.StartNew();
        long fact20bis = 0;
        for (var i = 0; i < N_fact; i++)
        {
            fact20bis = (long)closure.Invoke(20L);
        }
        sw.Stop();
        elapsed = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"(LinearLisp) 20! = {fact20bis} x {N_fact:0,0} times ... in {elapsed:0,0} ms");

        System.Diagnostics.Debug.Assert(fact20 == fact20bis);

        System.Console.WriteLine();
        System.Console.WriteLine("C# compiled vs LinearLisp, getting all the primes below 15,485,865...");
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        result = evaluator.Evaluate(null, @"
( let
    (
        thePrimes ( ... )
        GetPrimesBelow ( ( n ) =>
        (
            let ( prime ( true [] n ) )
            ( ( 2 < n ) ?
                (
                    ( ( prime @ 1 ) = false ), ( ( prime @ 0 ) = false ),
                    ( i = 2 ),
                    ( while ( ( i ** 2 ) < n ) (
                        (
                            ( prime @ i ) ?
                            (
                                ( j = i ),
                                ( while ( ( j * i ) < n ) (
                                    ( ( prime @ ( j * i ) ) = false ), ( j += 1 ) )
                                ) )
                            :
                            prime
                        ),
                        ( i += 1 )
                    ) ),
                    ( for p = 0, ( # prime ):
                        ( ( prime @ p ) ? ( thePrimes : p ) : thePrimes )
                    )
                )
            : thePrimes )
        ) )
    ) ( ( ) => ( ( GetPrimesBelow 15485865 ), thePrimes ) ) )");
        var getPrimes = (Closure)result;

        sw = System.Diagnostics.Stopwatch.StartNew();
        var primes = CompiledGetPrimesBelow(15_485_865L);
        sw.Stop();
        ms = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"(C# compiled) found {primes.Count} primes ... in {ms:0,0} ms");
        System.Console.WriteLine($"first... {primes[0]}");
        System.Console.WriteLine($"last ... {primes[primes.Count - 1]}");

        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        sw = System.Diagnostics.Stopwatch.StartNew();
        var primesBis = (List<object>)getPrimes.Invoke();
        sw.Stop();
        ms = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"(LinearLisp) found {primesBis.Count} primes ... in {ms:0,0} ms");
        System.Console.WriteLine($"first... {primesBis[0]}");
        System.Console.WriteLine($"last ... {primesBis[primesBis.Count - 1]}");

        System.Diagnostics.Debug.Assert(primesBis.Cast<long>().SequenceEqual(primes));

        /*System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        var _16kInts = System.IO.File.ReadAllText(@"16K-INTS.txt").Split(",").Select(s => long.Parse(s)).ToArray();
        var _10kIntsList = new List<long>(_16kInts);
        _10kIntsList.Sort();
        var sorted16kInts = _10kIntsList.ToArray();

        System.Console.WriteLine();
        System.Console.WriteLine($"C# compiled vs LinearLisp bubble-sort of {_16kInts.Length} integers...");
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        void CompiledBubbleSort(long[] array)
        {
            var more = true;
            for (var i = 1; (i <= (array.Length - 1)) && more; i++)
            {
                more = false;
                for (var j = 0; j < (array.Length - 1); j++)
                {
                    if (array[j + 1] < array[j])
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        more = true;
                    }
                }
            }
        }

        var bubbleSort = (Closure)evaluator.Evaluate(null, @"(
    let
    (
        BubbleSort ( ( array ) =>
            (
                let ( more 1    i -1    temp -1 )
                (
                    ( i = 0 ),
                    ( while ( ( i = ( i + 1 ) ) , ( ( i < ( # array ) ) && ( 0 < more ) ) )
                        (
                            ( more = 0 ),
                            ( for j = 0, ( ( # array ) - 1 ):
                                (
                                    ( ( array @ ( j + 1 ) ) < ( array @ j ) ) ?
                                    (
                                        ( temp = ( array @ j ) ),
                                        ( ( array @ j ) = ( array @ ( j + 1 ) ) ),
                                        ( ( array @ ( j + 1 ) )  = temp  ),
                                        ( more = 1 )
                                    )
                                    :
                                    array
                                )
                            )
                        )
                    )
                )
            )
        )
    )

    BubbleSort
)  ");
        var _16kIntsCopy = new long[_16kInts.Length];
        System.Array.Copy(_16kInts, _16kIntsCopy, _16kInts.Length);
        sw = System.Diagnostics.Stopwatch.StartNew();
        CompiledBubbleSort(_16kIntsCopy);
        sw.Stop();
        ms = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"(C# compiled) bubble-sort of {_16kIntsCopy.Length} integers in {ms:0,0} ms...");
        System.Console.WriteLine($"first... {_16kIntsCopy[0]}");
        System.Console.WriteLine($"last ... {_16kIntsCopy[_16kIntsCopy.Length - 1]}");
        System.Diagnostics.Debug.Assert(_16kIntsCopy[0] == 56_051L);
        System.Diagnostics.Debug.Assert(_16kIntsCopy[_16kIntsCopy.Length - 1] == 2_147_471_310L);
        System.Diagnostics.Debug.Assert(_16kIntsCopy.SequenceEqual(sorted16kInts));

        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        System.Array.Copy(_16kInts, _16kIntsCopy, _16kInts.Length);
        sw = System.Diagnostics.Stopwatch.StartNew();
        bubbleSort.Invoke(_16kIntsCopy);
        sw.Stop();
        ms = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"(LinearLisp) bubble-sort of {_16kIntsCopy.Length} integers in {ms:0,0} ms...");
        System.Console.WriteLine($"first... {_16kIntsCopy[0]}");
        System.Console.WriteLine($"last ... {_16kIntsCopy[_16kIntsCopy.Length - 1]}");
        System.Diagnostics.Debug.Assert(_16kIntsCopy[0] == 56_051L);
        System.Diagnostics.Debug.Assert(_16kIntsCopy[_16kIntsCopy.Length - 1] == 2_147_471_310L);
        System.Diagnostics.Debug.Assert(_16kIntsCopy.SequenceEqual(sorted16kInts));*/

        System.Console.WriteLine();
        System.Console.WriteLine("Press a key... to exit LinearLisp");
        System.Console.ReadKey(true);
    }
}
